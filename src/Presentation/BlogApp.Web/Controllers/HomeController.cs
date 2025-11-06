using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.ServiceCategory;
using BlogApp.Application.Features.BlogPosts.Commands.IncrementViewCount;
using BlogApp.Application.Features.BlogPosts.Queries.GetPublishedPosts;
using BlogApp.Application.Features.HomePageSection.Quaries.GetHomePageSection;
using BlogApp.Application.Features.ServiceCategories.Queries.GetHomePageServiceCategories;
using BlogApp.Web.Models;
using BlogApp.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel viewModel = new HomeViewModel
            {
                ServiceCategories = new List<ServiceCategoryDto>(),
                LatestBlogPosts = new List<BlogPostDto>()
            };
            try
            {
                var homePageSectionQuery = new GetPageSectionQuery { SectionKey = "home"};
                var homePageSectionResult = await _mediator.Send(homePageSectionQuery);
                if(homePageSectionResult.IsSuccess)
                {
                    viewModel.Section = homePageSectionResult.Data ?? new PageSectionDto();
                }

                // Get home page services
                var servicesQuery = new GetHomePageServiceCategoriesQuery();
                var servicesResult = await _mediator.Send(servicesQuery);

                if (servicesResult.IsSuccess)
                {
                    viewModel.ServiceCategories = servicesResult.Data ?? new List<ServiceCategoryDto>();
                }

                // Get latest blog posts
                var blogQuery = new GetPublishedPostsQuery { Page = 1, PageSize = 3 };
                var blogResult = await _mediator.Send(blogQuery);

                if (blogResult.IsSuccess)
                {
                    viewModel.LatestBlogPosts = blogResult.Data ?? new List<BlogPostDto>();
                }

                await _mediator.Send(new IncrementViewCountCommand { Id =  viewModel.Section?.Id});

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading home page data");
            }

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
