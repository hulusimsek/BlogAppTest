using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.ServiceCategory;
using BlogApp.Application.Features.BlogPosts.Queries.GetPublishedPosts;
using BlogApp.Application.Features.HomePageSection.Quaries.GetHomePageSection;
using BlogApp.Application.Features.ServiceCategories.Queries.GetAllActiveServiceCategories;
using BlogApp.Application.Features.ServiceCategories.Queries.GetHomePageServiceCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Xml.Linq;

namespace BlogApp.Web.Controllers
{
    [Route("sitemap.xml")]
    public class SitemapController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;

        public SitemapController(ILogger<HomeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            // 🧱 1️⃣ Statik sayfalar
            var staticUrls = new[]
            {
                new { Loc = $"{baseUrl}/", Priority = "1.0" },
                new { Loc = $"{baseUrl}/hakkimizda", Priority = "0.8" },
                new { Loc = $"{baseUrl}/hizmetler", Priority = "0.8" },
                new { Loc = $"{baseUrl}/blog", Priority = "0.8" },
                new { Loc = $"{baseUrl}/iletisim", Priority = "0.6" }
            };

            List<BlogPostDto> blogPosts = new List<BlogPostDto>();
            List<ServiceCategoryDto> services = new List<ServiceCategoryDto>();

            var blogPostsQuery = new GetPublishedPostsQuery { Page = 1, PageSize = 100 };
            var blogPostsQueryResult = await _mediator.Send(blogPostsQuery);
            if (blogPostsQueryResult.IsSuccess)
            {
                blogPosts = blogPostsQueryResult.Data ?? new List<BlogPostDto>();
            }

            // 📰 2️⃣ Blog yazıları (dinamik)
            var blogUrls = blogPosts.Select(post => new
            {
                Loc = $"{baseUrl}/blog/{post.Slug}",
                LastMod = post.ModifiedDate ?? post.CreatedDate,
                Priority = "0.7"
            });

            var servicesQuery = new GetAllActiveServiceCategoriesQuery();
            var servicesResult = await _mediator.Send(servicesQuery);

            if (servicesResult.IsSuccess)
            {
                services = servicesResult.Data ?? new List<ServiceCategoryDto>();
            }

            // ⚖️ 3️⃣ Hizmet kategorileri (dinamik)
            var serviceUrls = services.Select(s => new
            {
                Loc = $"{baseUrl}/hizmetler/{s.Slug}",
                LastMod = s.ModifiedDate ?? s.CreatedDate,
                Priority = "0.8"
            });

            // 🔗 4️⃣ XML yapısı oluştur
            var allUrls = staticUrls
                .Select(u => new XElement("url",
                    new XElement("loc", u.Loc),
                    new XElement("lastmod", DateTime.UtcNow.ToString("yyyy-MM-dd")),
                    new XElement("changefreq", "weekly"),
                    new XElement("priority", u.Priority)
                ))
                .Concat(blogUrls.Select(u => new XElement("url",
                    new XElement("loc", u.Loc),
                    new XElement("lastmod", u.LastMod.ToString("yyyy-MM-dd")),
                    new XElement("changefreq", "monthly"),
                    new XElement("priority", u.Priority)
                )))
                .Concat(serviceUrls.Select(u => new XElement("url",
                    new XElement("loc", u.Loc),
                    new XElement("lastmod", u.LastMod.ToString("yyyy-MM-dd")),
                    new XElement("changefreq", "monthly"),
                    new XElement("priority", u.Priority)
                )));

            var urlset = new XElement(XName.Get("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9"),
                allUrls
            );


            var xml = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), urlset);
            var xmlString = xml.ToString(SaveOptions.DisableFormatting);

            return Content(xmlString, "application/xml", Encoding.UTF8);
        }
    }
}
