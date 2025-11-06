using BlogApp.Application.DTOs.Contact;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactInfo.Commands.UpdateContactInfo
{
    public class UpdateContactInfoCommandValidator : AbstractValidator<UpdateContactInfoCommand>
    {
        public UpdateContactInfoCommandValidator()
        {
            // Adres zorunlu ve boş olamaz
            RuleFor(x => x.Data.Address)
                .NotEmpty().WithMessage("Adres boş olamaz.")
                .MaximumLength(500).WithMessage("Adres 500 karakterden uzun olamaz.");

            // Telefon zorunlu ve geçerli bir telefon numarası olmalı
            RuleFor(x => x.Data.Phone)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                .MaximumLength(50).WithMessage("Telefon numarası 50 karakterden uzun olamaz.")
                .Matches(@"^\+?\d{1,4}[\s-]?\(?\d{1,5}\)?[\s-]?\d{1,5}[\s-]?\d{1,5}$")
                .WithMessage("Geçerli bir telefon numarası girin."); // Basit telefon numarası regexi, ihtiyaca göre geliştirilebilir

            // Email zorunlu ve geçerli bir e-posta adresi olmalı
            RuleFor(x => x.Data.Email)
                .NotEmpty().WithMessage("E-posta adresi boş olamaz.")
                .MaximumLength(200).WithMessage("E-posta 200 karakterden uzun olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi girin.");

            // MapEmbedUrl isteğe bağlı ancak eğer varsa geçerli bir URL olmalı
            RuleFor(x => x.Data.MapEmbedUrl)
                .MaximumLength(1000).WithMessage("Harita URL'si 1000 karakterden uzun olamaz.")
                .Must(BeAValidUrl).WithMessage("Geçerli bir harita URL'si girin.")
                .When(x => !string.IsNullOrWhiteSpace(x.Data.MapEmbedUrl)); // Eğer boş değilse kontrol et

            RuleFor(x => x.Data.StreetAddress)
                .MaximumLength(500).WithMessage("Sokak adresi 500 karakterden uzun olamaz.")
                .When(x => !string.IsNullOrWhiteSpace(x.Data.StreetAddress)); // Eğer boş değilse kontrol et

            // İlçe (isteğe bağlı, boş olabilir)
            RuleFor(x => x.Data.AddressLocality)
                .MaximumLength(100).WithMessage("İlçe adı 100 karakterden uzun olamaz.")
                .When(x => !string.IsNullOrWhiteSpace(x.Data.AddressLocality)); // Eğer boş değilse kontrol et

            // İl (isteğe bağlı, boş olabilir)
            RuleFor(x => x.Data.AddressRegion)
                .MaximumLength(100).WithMessage("İl adı 100 karakterden uzun olamaz.")
                .When(x => !string.IsNullOrWhiteSpace(x.Data.AddressRegion)); // Eğer boş değilse kontrol et

            // Posta kodu (isteğe bağlı, boş olabilir)
            RuleFor(x => x.Data.PostalCode)
                .MaximumLength(20).WithMessage("Posta kodu 20 karakterden uzun olamaz.")
                .When(x => !string.IsNullOrWhiteSpace(x.Data.PostalCode)); // Eğer boş değilse kontrol et

            // Ülke (isteğe bağlı, boş olabilir)
            RuleFor(x => x.Data.AddressCountry)
                .MaximumLength(100).WithMessage("Ülke adı 100 karakterden uzun olamaz.")
                .When(x => !string.IsNullOrWhiteSpace(x.Data.AddressCountry)); // Eğer boş değilse kontrol et

            // Çalışma saatleri listesi
            RuleForEach(x => x.Data.WorkingHours).SetValidator(new WorkingHourValidator());

        }

        // URL kontrolü yapan yardımcı fonksiyon
        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }

    public class WorkingHourValidator : AbstractValidator<WorkingHourDto>
    {
        public WorkingHourValidator()
        {
            RuleFor(x => x.DayNameEn)
                .NotEmpty().WithMessage("Günün İngilizce adı boş olamaz.")
                .MaximumLength(50);

            RuleFor(x => x.DayNameTr)
                .NotEmpty().WithMessage("Günün Türkçe adı boş olamaz.")
                .MaximumLength(50);

            // Eğer kapalı değilse saatler doğrulanmalı
            When(x => x.IsClosed == false, () =>
            {
                RuleFor(x => x.Opens)
                    .NotEmpty().WithMessage("Açılış saati boş olamaz.")
                    .Must(BeValidTime).WithMessage("Açılış saati 'HH:mm' formatında olmalıdır.");

                RuleFor(x => x.Closes)
                    .NotEmpty().WithMessage("Kapanış saati boş olamaz.")
                    .Must(BeValidTime).WithMessage("Kapanış saati 'HH:mm' formatında olmalıdır.");

                // Açılış < Kapanış kontrolü
                RuleFor(x => x)
                    .Must(dto =>
                    {
                        if (!BeValidTime(dto.Opens) || !BeValidTime(dto.Closes))
                            return false;

                        var open = TimeSpan.ParseExact(dto.Opens, "hh\\:mm", CultureInfo.InvariantCulture);
                        var close = TimeSpan.ParseExact(dto.Closes, "hh\\:mm", CultureInfo.InvariantCulture);
                        return open < close;
                    })
                    .WithMessage("Açılış saati kapanış saatinden önce olmalıdır.");
            });

            RuleFor(x => x.IsClosed)
                .NotNull().WithMessage("Kapalı mı alanı belirtilmelidir.");
        }

        private bool BeValidTime(string time)
        {
            if (string.IsNullOrWhiteSpace(time)) return false;
            // Kabul edilen format: "09:00"
            return TimeSpan.TryParseExact(time, "hh\\:mm", CultureInfo.InvariantCulture, out _)
                || TimeSpan.TryParseExact(time, "h\\:mm", CultureInfo.InvariantCulture, out _);
        }
    }


}
