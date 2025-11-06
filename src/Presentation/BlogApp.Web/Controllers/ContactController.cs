using BlogApp.Application.Features.ContactInfo.Queries.Commands;
using BlogApp.Application.Features.HomePageSection.Quaries.GetHomePageSection;
using BlogApp.Application.Features.SiteSettings.Queries.GetSiteSettings;
using BlogApp.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.Controllers
{
    [Route("iletisim")]
    public class ContactController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IMediator mediator, ILogger<ContactController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var viewModel = new ContactViewModel();

                var contactInfoResult = await _mediator.Send(new GetContactInfoQuery());
                if(contactInfoResult.IsSuccess)
                {
                    viewModel.ContactInfo = contactInfoResult.Data;
                }

                var siteSettingsResult = await _mediator.Send(new GetSiteSettingsQuery());
                if (siteSettingsResult.IsSuccess)
                {
                    viewModel.SiteSettings = siteSettingsResult.Data;
                }

                var sectionResult = await _mediator.Send(new GetPageSectionQuery { SectionKey = "contactInfo"});
                if (sectionResult.IsSuccess)
                {
                    viewModel.Section = sectionResult.Data;
                }

                if (viewModel.ContactInfo?.WorkingHours != null)
                {
                    viewModel.ContactInfo.WorkingHours = viewModel.ContactInfo.WorkingHours
                        .OrderBy(x => x.DayNameTr switch
                        {
                            "Pazartesi" => 1,
                            "Salı" => 2,
                            "Çarşamba" => 3,
                            "Perşembe" => 4,
                            "Cuma" => 5,
                            "Cumartesi" => 6,
                            "Pazar" => 7,
                            _ => 8
                        })
                        .ToList();
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading contact page");
                TempData["ErrorMessage"] = "İletişim bilgileri yüklenirken bir hata oluştu.";
                return View(new ContactViewModel());
            }
        }
    }
}
