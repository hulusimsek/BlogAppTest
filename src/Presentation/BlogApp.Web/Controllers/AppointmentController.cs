using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.ServiceCategory;
using BlogApp.Application.Features.ContactFormSettings.Queries.GetContactFormSettings;
using BlogApp.Application.Features.ContactInfo.Queries.Commands;
using BlogApp.Application.Features.ContactRequests.Commands.CreateContactRequest;
using BlogApp.Application.Features.HomePageSection.Quaries.GetHomePageSection;
using BlogApp.Application.Features.ServiceCategories.Queries.GetActiveMinimalCategories;
using BlogApp.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.Controllers
{
    [Route("online-danismanlik")]
    public class AppointmentController : BaseController
    {
        private readonly ILogger<BaseController> _logger;
        private readonly IMediator _mediator;

        public AppointmentController(ILogger<BaseController> logger, IMediator mediator)
                : base(logger, mediator)  // BaseController constructor’ına geçiriyoruz
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            var viewModel = new AppointmentViewModel
            {
                Categories = new List<MinimalCategoryDto>(),
                ContactFormSettings = new ContactFormSettingsDto()
            };

            try
            {
                await LoadAppAppointmentData(viewModel);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading appointment page");
                return NotFound();
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SendContactRequest(AppointmentViewModel model)
        {
            // 1. Temel kontroller
            if (model?.ContactRequest == null)
            {
                _logger.LogWarning("ContactRequest model is null");
                TempData["ErrorMessage"] = "Form verileri alınamadı. Lütfen tekrar deneyin.";
                return RedirectToAction("Index");
            }

            // 3. ModelState kontrolü
            ModelState.Clear();

            await LoadAppAppointmentData(model);

            if(model?.ContactRequest.KvkkAccepted == false)
            {
                TempData["ErrorMessage"] = "KVVK şartlarını onaylayıp lütfen tekrar deneyin.";
                return View("Index", model);
            }

            try
            {
                // 4. Command gönder
                var command = new CreateContactRequestCommand { Data = model!.ContactRequest };
                var result = await _mediator.Send(command);

                if (result.IsSuccess)
                {
                    TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi. En kısa sürede sizinle iletişime geçeceğiz.";
                    return RedirectToAction("Index");
                }


                // 3. ModelState kontrolü
                ModelState.Clear();

                // İşlem başarısız
                HandleFailedResult(result);

                return View("Index", model);
            }
            catch (FluentValidation.ValidationException vex)
            {
                HandleValidationException(vex);
                return View("Index", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in SendContactRequest AppoinmentController SendContactRequest");
                TempData["ErrorMessage"] = "Beklenmeyen bir hata oluştu. Lütfen tekrar deneyin.";
                return View("Index", model);
            }
        }

        private async Task LoadAppAppointmentData(AppointmentViewModel viewModel)
        {
            try
            {
                var serviceCategoriesQuery = new GetActiveMinimalCategoriesQuery();
                var serviceCategoriesResult = await _mediator.Send(serviceCategoriesQuery);

                if (serviceCategoriesResult.IsSuccess && serviceCategoriesResult.Data != null)
                {
                    viewModel.Categories = serviceCategoriesResult.Data;
                }

                var queryFormSettings = new GetContactFormSettingsQuery { SectionKey = "appointment" };
                var formSettingsResult = await _mediator.Send(queryFormSettings);

                if (formSettingsResult.IsSuccess && formSettingsResult.Data != null)
                {
                    viewModel.ContactFormSettings = formSettingsResult.Data!;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Form verileri alınırken bir hata oluştu.";
            }
        }
    }
}
