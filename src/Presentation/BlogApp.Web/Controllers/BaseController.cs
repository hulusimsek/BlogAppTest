using BlogApp.Application.Common;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ILogger<BaseController> Logger;
        protected readonly IMediator Mediator;

        protected BaseController(ILogger<BaseController> logger, IMediator mediator)
        {
            Logger = logger;
            Mediator = mediator;
        }

        protected void HandleValidationException(ValidationException vex)
        {
            TempData["ScrollToForm"] = true;

            var distinctErrors = vex.Errors
                .GroupBy(e => e.PropertyName)
                .Select(g => g.First());

            foreach (var error in distinctErrors)
            {
                var propertyName = error.PropertyName.StartsWith("Data.")
                    ? error.PropertyName.Replace("Data.", "")
                    : error.PropertyName;

                ModelState.AddModelError(propertyName, error.ErrorMessage);
            }
        }

        protected void HandleFailedResult(AppResult result)
        {
            TempData["ScrollToForm"] = true;

            if (result.Errors != null && result.Errors.Count > 0)
            {
                var errors = result.Errors as IEnumerable<string> ?? new List<string>();
                TempData["ErrorMessage"] = string.Join("<br/>", errors);
            }
            else
            {
                var errorMessage = result.ErrorMessage;
                TempData["ErrorMessage"] = $"Mesaj gönderilirken bir hata oluştu. Lütfen tekrar deneyin." +
                    (string.IsNullOrEmpty(errorMessage) ? "" : $"<br/>{errorMessage}");  // errorMessage alt satıra eklenir
            }
        }

        protected void SetSuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }

        protected void SetErrorMessage(string message)
        {
            TempData["ScrollToForm"] = true;
            TempData["ErrorMessage"] = message;
        }
    }

}
