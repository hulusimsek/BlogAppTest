using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.ServiceCategory;
using BlogApp.Application.Features.ContactRequests.Commands.CreateContactRequest;
using BlogApp.Application.Features.HomePageSection.Quaries.GetHomePageSection;
using BlogApp.Application.Features.ServiceCategories.Queries.GetAllActiveServiceCategories;
using BlogApp.Application.Features.ServiceCategories.Queries.GetHomePageServiceCategories;
using BlogApp.Application.Features.ServiceCategories.Queries.GetServiceCategoryBySlug;
using System.Linq;
using BlogApp.Web.ViewModels;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BlogApp.Application.Features.ContactFormSettings.Queries.GetContactFormSettings;
using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.Features.ContactInfo.Queries.Commands;
using System.Security.Cryptography.Xml;

namespace BlogApp.Web.Controllers
{
    [Route("hizmetler")]
    public class ServicesController : BaseController
    {
        private readonly ILogger<BaseController> _logger;
        private readonly IMediator _mediator;

        public ServicesController(ILogger<BaseController> logger, IMediator mediator)
            : base(logger, mediator)  // BaseController constructor’ına geçiriyoruz
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new ServicesViewModel
            {
                ServiceCategories = new List<ServiceCategoryDto>(),
            };

            try
            {
                var servicesPageSectionQuery = new GetPageSectionQuery { SectionKey = "services" };
                var servicesPageSectionResult = await _mediator.Send(servicesPageSectionQuery);
                if (servicesPageSectionResult.IsSuccess)
                {
                    viewModel.Section = servicesPageSectionResult.Data ?? new PageSectionDto();
                }

                // Get home page services
                var servicesQuery = new GetAllActiveServiceCategoriesQuery();
                var servicesResult = await _mediator.Send(servicesQuery);

                if (servicesResult.IsSuccess)
                {
                    viewModel.ServiceCategories = servicesResult.Data ?? new List<ServiceCategoryDto>();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading home page data");
            }

            return View(viewModel);
        }

        [HttpGet("{slug}", Name = "ServiceDetail")]
        public async Task<IActionResult> Detail(string slug)
        {
            try
            {
                var viewModel = new ServiceDetailViewModel();
                await LoadServiceCategoryAsync(viewModel, slug);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading service detail page for slug: {Slug}", slug);
                return NotFound();
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SendContactRequest(ServiceDetailViewModel model, string slug)
        {
            // 1. Temel kontroller
            if (model?.ContactRequest == null)
            {
                _logger.LogWarning("ContactRequest model is null");
                TempData["ErrorMessage"] = "Form verileri alınamadı. Lütfen tekrar deneyin.";
                return RedirectToAction("Detail", new { slug });
            }

            // 2. ServiceCategory bilgisini doldur
            await LoadServiceCategoryAsync(model, slug ?? model.ServiceCategory?.Slug);

            // 3. ModelState kontrolü
            ModelState.Clear();

            if (model?.ContactRequest.KvkkAccepted == false)
            {
                TempData["ErrorMessage"] = "KVVK şartlarını onaylayıp lütfen tekrar deneyin.";
                TempData["ScrollToForm"] = true;
                return View("Detail", model);
            }

            try
            {
                // 4. Command gönder
                var command = new CreateContactRequestCommand { Data = model.ContactRequest };
                var result = await _mediator.Send(command);

                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi. En kısa sürede sizinle iletişime geçeceğiz.";
                    return RedirectToAction("Detail", new { slug = model.ServiceCategory?.Slug ?? slug });
                }

                // İşlem başarısız
                HandleFailedResult(result);
                TempData["ScrollToForm"] = true;

                return View("Detail", model);
            }
            catch (FluentValidation.ValidationException vex)
            {
                HandleValidationException(vex);
                TempData["ScrollToForm"] = true;
                return View("Detail", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in SendContactRequest for slug: {Slug}", slug);
                TempData["ErrorMessage"] = "Beklenmeyen bir hata oluştu. Lütfen tekrar deneyin.";
                TempData["ScrollToForm"] = true;
                return View("Detail", model);
            }
        }
        private async Task LoadServiceCategoryAsync(ServiceDetailViewModel model, string? slug)
        {
            try
            {
                var querySection = new GetPageSectionQuery { SectionKey = "serviceDetail" };
                var sectionResult = await _mediator.Send(querySection);

                if (sectionResult.IsSuccess && sectionResult.Data != null)
                {
                    model.ServiceDetailPageSection = sectionResult.Data;
                }

                var queryFormSettings = new GetContactFormSettingsQuery { SectionKey = "serviceDetail" };
                var formSettingsResult = await _mediator.Send(queryFormSettings);

                if (formSettingsResult.IsSuccess && formSettingsResult.Data != null)
                {
                    model.ContactFormSettings = formSettingsResult.Data;
                }


                var queryContactInfo = new GetContactInfoQuery();
                var contactInfoResult = await _mediator.Send(queryContactInfo);

                if (contactInfoResult.IsSuccess && contactInfoResult != null)
                {
                    model.ContactInfo = contactInfoResult.Data;
                }

                

                var effectiveSlug = slug ?? model.ServiceCategory?.Slug;

                if (string.IsNullOrEmpty(effectiveSlug))
                {
                    _logger.LogWarning("Slug is null or empty");
                    model.ServiceCategory = new ServiceCategoryDetailDto();
                    return;
                }

                var query = new GetServiceCategoryBySlugQuery { Slug = effectiveSlug };
                var result = await _mediator.Send(query);

                if (result.IsSuccess && result.Data != null)
                {
                    model.ServiceCategory = result.Data;
                    ViewData["BreadcrumbTitle"] = result.Data.Name;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading service category for slug: {Slug}", slug);
                model.ServiceCategory = new ServiceCategoryDetailDto();
                TempData["ErrorMessage"] = "Hizmet verileri alınırken bir hata oluştu.";
            }
        }
    }
}
