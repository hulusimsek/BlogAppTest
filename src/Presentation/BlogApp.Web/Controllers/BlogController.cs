using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.ServiceCategory;
using BlogApp.Application.DTOs.Tag;
using BlogApp.Application.Features.BlogComments.Commands.CreateBlogComment;
using BlogApp.Application.Features.BlogPosts.Commands.IncrementViewCount;
using BlogApp.Application.Features.BlogPosts.Queries.Filter;
using BlogApp.Application.Features.BlogPosts.Queries.GetAllBlogPosts;
using BlogApp.Application.Features.BlogPosts.Queries.GetBlogPostBySlug;
using BlogApp.Application.Features.BlogPosts.Queries.GetBlogPostWithDetailById;
using BlogApp.Application.Features.BlogPosts.Queries.GetPostsByCategory;
using BlogApp.Application.Features.BlogPosts.Queries.GetPublishedPosts;
using BlogApp.Application.Features.HomePageSection.Quaries.GetHomePageSection;
using BlogApp.Application.Features.ServiceCategories.Queries.GetActiveMinimalCategories;
using BlogApp.Application.Features.Tags.Queries.GetAllTagsQuery;
using BlogApp.Domain.Entities;
using BlogApp.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using static System.Collections.Specialized.BitVector32;

namespace BlogApp.Web.Controllers
{
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: /Blog
        [HttpGet]
        public async Task<IActionResult> Index(string? categorySlug = null, string? tag = null, string? searchTitle = null,
                                                    string? sortBy = "PublishDate", bool descending = true, int page = 1, int pageSize = 9)
        {
            var blogSection = await GetPageSection();
            BlogViewModel viewModel = new BlogViewModel
            {
                Section = blogSection,
                BlogPosts = new List<BlogPostDto>(),
                PopularBlogPosts = new List<BlogPostDto>(),
                Categories = new List<MinimalCategoryDto>(),
                CurrentCategorySlug = categorySlug,
                SearchTitle = searchTitle,
                SortBy = sortBy,
                Descending = descending,
                CurrentTag = tag,
                CurrentPage = page,
                PageSize = pageSize
            };

            var blogData = await GetBlogData(section: "blogs", categorySlug, tag, searchTitle, sortBy, descending, page, pageSize);

            if (blogData != null)
            {
                viewModel.BlogPosts = blogData.BlogPosts;
                viewModel.PopularBlogPosts = blogData.PopularBlogPosts;
                viewModel.Categories = blogData.Categories;
                viewModel.CurrentCategoryName = blogData.CurrentCategoryName;
                viewModel.Tags = blogData.Tags;
                viewModel.CurrentTagName = blogData.CurrentTagName;
                viewModel.TotalPages = blogData.TotalPages;
                viewModel.TotalCount = blogData.TotalCount;
                viewModel.Section = blogData.Section;
                viewModel.AllTotalCount = blogData.AllTotalCount;

            }
            else
            {
                TempData["ErrorMessage"] = "Blog yazıları yüklenirken bir hata oluştu.";
            }
            return View(viewModel);
        }

        [HttpGet("kategori/{slug}")]
        public async Task<IActionResult> Category(string slug, int page = 1, int pageSize = 9)
        {
            return await Index(categorySlug: slug, page: page, pageSize: pageSize)
                    is ViewResult vr ? View("Index", vr.Model) : RedirectToAction("Index");

        }

        [HttpGet("tag/{slug}")]
        public async Task<IActionResult> Tag(string slug, int page = 1, int pageSize = 9)
        {
            return await Index(tag: slug, page: page, pageSize: pageSize)
                is ViewResult vr ? View("Index", vr.Model) : RedirectToAction("Index");
        }

        [HttpGet("kategori")]
        public async Task<IActionResult> AllCategories()
        {
            BlogViewModel viewModel = new BlogViewModel
            {
                BlogPosts = new List<BlogPostDto>(),
                PopularBlogPosts = new List<BlogPostDto>(),
                Categories = new List<MinimalCategoryDto>()
            };

            try
            {
                var sideData = await GetAsideBarData("categories");

                if (sideData != null)
                {
                    viewModel.PopularBlogPosts = sideData.PopularBlogPosts;
                    viewModel.Categories = sideData.Categories;
                    viewModel.Tags = sideData.Tags;
                    viewModel.Section = sideData.Section;
                }

                return View("AllCategories", viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Blog yazıları yüklenirken bir hata oluştu.";
                return View("AllCategories", viewModel);
            }
        }

        [HttpGet("tag")]
        public async Task<IActionResult> AllTags()
        {
            BlogViewModel viewModel = new BlogViewModel
            {
                BlogPosts = new List<BlogPostDto>(),
                PopularBlogPosts = new List<BlogPostDto>(),
                Categories = new List<MinimalCategoryDto>()
            };

            try
            {
                var sideData = await GetAsideBarData("tags");

                if (sideData != null)
                {
                    viewModel.PopularBlogPosts = sideData.PopularBlogPosts;
                    viewModel.Categories = sideData.Categories;
                    viewModel.Tags = sideData.Tags;
                    viewModel.Section = sideData.Section;
                }

                return View("AllTags", viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Blog yazıları yüklenirken bir hata oluştu."; 
                return View("AllTags", viewModel);
            }
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> Detail(string slug)
        {
            try
            {
                // Blog yazısını slug ile çek
                var query = new GetBlogPostBySlugQuery { Slug = slug };
                var result = await _mediator.Send(query);

                if (!result.IsSuccess || result.Data == null)
                {
                    TempData["ErrorMessage"] = "Blog yazısı bulunamadı.";
                    return RedirectToAction("Index");
                }

                // Görüntülenme sayısını artır
                var incrementCommand = new IncrementViewCountCommand { Id = result.Data.Id };
                await _mediator.Send(incrementCommand);

                // Sidebar verileri
                var sideData = await GetAsideBarData("blogs");

                // İlgili blog yazıları (aynı kategorideki diğer yazılar)
                List<BlogPostDto> relatedPosts = new List<BlogPostDto>();
                if (!string.IsNullOrEmpty(result.Data.CategorySlug))
                {
                    var relatedQuery = new GetPostsByCategoryQuery
                    {
                        categorySlug = result.Data.CategorySlug,
                        PageSize = 3,
                        ExcludePostId = result.Data.Id
                    };
                    var relatedResult = await _mediator.Send(relatedQuery);
                    if (relatedResult.IsSuccess)
                    {
                        relatedPosts = relatedResult.Data ?? new List<BlogPostDto>();
                    }
                }

                var viewModel = new BlogDetailViewModel
                {
                    BlogPost = result.Data,
                    RelatedPosts = relatedPosts,
                    PopularBlogPosts = sideData?.PopularBlogPosts ?? new List<BlogPostDto>(),
                    Categories = sideData?.Categories ?? new List<MinimalCategoryDto>(),
                    Tags = sideData?.Tags ?? new List<TagDto>(),
                    Section = sideData?.Section
                };

                // SEO için ViewData ayarları
                ViewData["Title"] = result.Data.MetaTitle ?? result.Data.Title;
                ViewData["MetaDescription"] = result.Data.MetaDescription ?? result.Data.Summary;
                ViewData["BreadcrumbTitle"] = result.Data.Title;


                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Blog yazısı yüklenirken bir hata oluştu.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost("{slug}/yorum")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(string slug, CreateBlogCommentCommand command)
        {
            try
            {
                // Blog yazısını bul
                var query = new GetBlogPostBySlugQuery { Slug = slug };
                var result = await _mediator.Send(query);

                if (!result.IsSuccess || result.Data == null)
                {
                    return Json(new { success = false, message = "Blog yazısı bulunamadı." });
                }

                command.Data.BlogPostId = result.Data.Id;
                var commentResult = await _mediator.Send(command);

                if (commentResult.IsSuccess)
                {
                    return Json(new
                    {
                        success = true,
                        message = "Yorumunuz başarıyla gönderildi. Onaylandıktan sonra yayınlanacaktır."
                    });
                }

                return Json(new
                {
                    success = false,
                    message = "Yorum gönderilirken bir hata oluştu."
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Yorum gönderilirken bir hata oluştu."
                });
            }
        }

        private async Task<PageSectionDto?> GetPageSection()
        {
            try
            {
                var query = new GetPageSectionQuery { SectionKey = "blogs" };
                var result = await _mediator.Send(query);
                if(result.IsSuccess)
                {
                    return result.Data;
                }
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private async Task<BlogViewModel?> GetAsideBarData(string section)
        {
            BlogViewModel viewModel = new BlogViewModel
            {
                BlogPosts = new List<BlogPostDto>(),
                PopularBlogPosts = new List<BlogPostDto>(),
                Categories = new List<MinimalCategoryDto>()
            };

            try
            {
                // Popüler yazılar için ayrı sorgu
                var popularPostsQuery = new GetPublishedPostsQuery
                {
                    Page = 1,
                    PageSize = 5,
                    OrderBy = "ViewCount",
                    IsDescending = true
                };
                var popularPostsResult = await _mediator.Send(popularPostsQuery);

                if (popularPostsResult.IsSuccess)
                {
                    viewModel.PopularBlogPosts = popularPostsResult.Data ?? new List<BlogPostDto>();
                }

                // Kategorileri çekme
                var categoriesQuery = new GetActiveMinimalCategoriesQuery();
                var categoriesResult = await _mediator.Send(categoriesQuery);

                if (categoriesResult.IsSuccess)
                {
                    viewModel.Categories = categoriesResult.Data ?? new List<MinimalCategoryDto>();
                }

                var tagQuery = new GetAllTagsQuery();
                var tagQueryResult = await _mediator.Send(tagQuery);

                if (tagQueryResult.IsSuccess)
                {
                    viewModel.Tags = tagQueryResult.Data ?? new List<TagDto>();
                }

                var sectionQuery = new GetPageSectionQuery { SectionKey = section };
                var sectionResult = await _mediator.Send(sectionQuery);
                if (sectionResult.IsSuccess)
                {
                    viewModel.Section = sectionResult.Data;
                }

                return viewModel;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private async Task<BlogViewModel> GetBlogData(string section, string? categorySlug, string? tag, string? searchTitle, string? sortBy,
                                                                bool descending, int page, int pageSize)
        {
            BlogViewModel viewModel = new BlogViewModel
            {
                BlogPosts = new List<BlogPostDto>(),
                PopularBlogPosts = new List<BlogPostDto>(),
                Categories = new List<MinimalCategoryDto>()
            };

            try
            {
                // Filtrelenmiş blog yazılarını çekme
                var query = new FilterBlogPostsQuery
                {
                    CategorySlug = categorySlug,
                    Tag = tag,
                    PageNumber = page,
                    SearchTitle = searchTitle,
                    SortBy = sortBy,
                    Descending = descending,
                    PageSize = pageSize,
                    IsPublished = true
                };

                var result = await _mediator.Send(query);

                if (result.IsSuccess)
                {
                    viewModel.BlogPosts = result.Data ?? new List<BlogPostDto>();
                }

                var sideData = await GetAsideBarData(section);

                if (sideData != null)
                {
                    viewModel.PopularBlogPosts = sideData.PopularBlogPosts;
                    viewModel.Categories = sideData.Categories;
                    viewModel.CurrentCategoryName = string.IsNullOrEmpty(categorySlug) ? null : sideData.Categories
                                                    .FirstOrDefault(c => c.Slug == categorySlug)?.Name;
                    viewModel.Tags = sideData.Tags;
                    viewModel.CurrentTagName = string.IsNullOrEmpty(tag) ? null : sideData.Tags
                                .FirstOrDefault(c => c.Slug == tag)?.Name;
                    viewModel.Section = sideData.Section;
                }

                // Sayfalama bilgilerini hesapla
                viewModel.TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize);
                viewModel.TotalCount = viewModel.BlogPosts.Count;
                viewModel.AllTotalCount = result.TotalCount;

                return viewModel;
            }
            catch (Exception)
            {
                return null; // Hata durumunda null dönecek, hata mesajı controller'da gösterilecek
            }
        }

    }
}
