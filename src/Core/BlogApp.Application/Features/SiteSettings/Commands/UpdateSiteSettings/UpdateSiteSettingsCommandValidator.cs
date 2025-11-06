using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.SiteSettings.Commands.UpdateSiteSettings
{
    public class UpdateSiteSettingsCommandValidator : AbstractValidator<UpdateSiteSettingsCommand>
    {
        public UpdateSiteSettingsCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Ayarlar bulunamadı.");

            RuleFor(x => x.Data.SiteName)
                .NotEmpty().WithMessage("Site adı boş olamaz.")
                .MaximumLength(100).WithMessage("Site adı 100 karakterden uzun olamaz.");

            RuleFor(x => x.Data.LogoUrl)
                .NotEmpty().WithMessage("Logo URL boş olamaz.")
                .Must(BeAValidUrl).WithMessage("Logo URL geçerli bir adres olmalıdır.");

            RuleFor(x => x.Data.FooterText)
                .MaximumLength(500).WithMessage("Footer metni 500 karakterden uzun olamaz.");

            RuleFor(x => x.Data.TwitterUrl)
                .Must(BeAValidUrl).WithMessage("Twitter adresi geçerli bir URL olmalıdır.")
                .When(x => !string.IsNullOrWhiteSpace(x.Data.TwitterUrl));  // Boş olursa geçerli kabul et

            RuleFor(x => x.Data.LinkedInUrl)
                .Must(BeAValidUrl).WithMessage("LinkedIn adresi geçerli bir URL olmalıdır.")
                .When(x => !string.IsNullOrWhiteSpace(x.Data.LinkedInUrl));

            RuleFor(x => x.Data.FacebookUrl)
                .Must(BeAValidUrl).WithMessage("Facebook adresi geçerli bir URL olmalıdır.")
                .When(x => !string.IsNullOrWhiteSpace(x.Data.FacebookUrl));

            RuleFor(x => x.Data.InstagramUrl)
                .Must(BeAValidUrl).WithMessage("Instagram adresi geçerli bir URL olmalıdır.")
                .When(x => !string.IsNullOrWhiteSpace(x.Data.InstagramUrl));
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var result)
                   && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }
    }

}
