using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.ServiceCategory;
using BlogApp.Application.Features.BlogPosts.Queries.GetPublishedPosts;
using BlogApp.Application.Features.ContactInfo.Queries.Commands;
using BlogApp.Application.Features.HomePageSection.Quaries.GetHomePageSection;
using BlogApp.Application.Features.LawyerProfiles.Queries.GetActiveProfileFirst;
using BlogApp.Application.Features.LawyerProfiles.Queries.GetAllLawyerProfiles;
using BlogApp.Application.Features.ServiceCategories.Queries.GetHomePageServiceCategories;
using BlogApp.Application.Features.SiteSettings.Queries.GetSiteSettings;
using BlogApp.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.Controllers
{
    [Route("hakkimizda")]
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> _logger;
        private readonly IMediator _mediator;

        public AboutController(ILogger<AboutController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AboutViewModel viewModel = new AboutViewModel();
            try
            {
                var lawyerProfilesQuery = new GetActiveProfileFirstQuery();
                var lawyerProfilesResult = await _mediator.Send(lawyerProfilesQuery);
                if (lawyerProfilesResult.IsSuccess)
                {
                    viewModel.LawyerProfile = lawyerProfilesResult.Data;
                }

                // Get home page services
                var contactInfoQuery = new GetContactInfoQuery();
                var contactInfoResult = await _mediator.Send(contactInfoQuery);

                if (contactInfoResult.IsSuccess)
                {
                    viewModel.ContactInfo = contactInfoResult.Data;
                }

                // Get site settings
                var siteSettingsQuery = new GetSiteSettingsQuery();
                var siteSettingsResult = await _mediator.Send(siteSettingsQuery);

                if (siteSettingsResult.IsSuccess)
                {
                    viewModel.SiteSettings = siteSettingsResult.Data;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading home page data");
            }

            return View(viewModel);
        }
    }
}
