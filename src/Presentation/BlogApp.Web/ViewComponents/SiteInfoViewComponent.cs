using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.DTOs.SiteSettings;
using BlogApp.Application.Features.ContactInfo.Queries.Commands;
using BlogApp.Application.Features.ServiceCategories.Queries.GetHomePageServiceCategories;
using BlogApp.Application.Features.SiteSettings.Queries.GetSiteSettings;
using BlogApp.Domain.Entities;
using BlogApp.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static System.Collections.Specialized.BitVector32;

namespace BlogApp.Web.ViewComponents
{
    public class SiteInfoViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;
        private const string CACHE_KEY = "LayoutData";
        private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(1);

        public SiteInfoViewComponent(IMediator mediator, IMemoryCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<IViewComponentResult> InvokeAsync(string section)
        {
            // Cache'den kontrol et
            if (!_cache.TryGetValue(CACHE_KEY, out LayoutViewModel? layoutData))
            {
                layoutData = new LayoutViewModel();

                // 1️⃣ Site Ayarları çek
                var siteSettingsResult = await _mediator.Send(new GetSiteSettingsQuery());
                if (siteSettingsResult.IsSuccess && siteSettingsResult.Data != null)
                    layoutData.SiteSettings = siteSettingsResult.Data;

                var contactInfoResult = await _mediator.Send(new GetContactInfoQuery());
                if (contactInfoResult.IsSuccess && contactInfoResult.Data != null)
                    layoutData.ContactInfo = contactInfoResult.Data;

                // 2️⃣ Hizmet Kategorileri çek
                var servicesResult = await _mediator.Send(new GetHomePageServiceCategoriesQuery());
                if (servicesResult.IsSuccess && servicesResult.Data != null)
                    layoutData.Services = servicesResult.Data;

                // 3️⃣ Cache’e kaydet
                _cache.Set(CACHE_KEY, layoutData, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = CacheDuration,
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                });
            }

            // JSON-LD verisini oluştur
            var jsonLd = BuildJsonLd(layoutData?.SiteSettings, layoutData?.ContactInfo);

            // Layout'a aktar
            if(layoutData != null)
            {
                ViewContext.ViewData["StructuredData"] = jsonLd;
            }

            // section parametresine göre farklı view döndür
            return section switch
            {
                "Header" => View("Header", layoutData),
                "Footer" => View("Footer", layoutData),
                _ => View("Default", layoutData)
            };
        }

        private string BuildJsonLd(SiteSettingsDto? site, ContactInfoDto? contact)
        {
            if (site == null || contact == null)
            {
                return string.Empty;
            }
            var workingHoursJson = contact.WorkingHours != null
                ? string.Join(",", contact.WorkingHours
                    .Where(x => !x.IsClosed)
                    .Select(x => $@"{{
                    ""@type"": ""OpeningHoursSpecification"",
                    ""dayOfWeek"": ""{x.DayNameEn}"",
                    ""opens"": ""{x.Opens}"",
                    ""closes"": ""{x.Closes}""
                }}"))
                : string.Empty;

            return $@"
                    <script type='application/ld+json'>
                    {{
                      ""@context"": ""https://schema.org"",
                      ""@type"": ""LegalService"",
                      ""name"": ""{site?.SiteName}"",
                      ""image"": ""{site?.LogoUrl ?? "https://www.avahmet.com/images/logo.png"}"",
                      ""url"": ""{HttpContext.Request.Scheme}://{HttpContext.Request.Host}"",
                      ""telephone"": ""{contact?.Phone}"",
                      ""email"": ""{contact?.Email}"",
                      ""address"": {{
                        ""@type"": ""PostalAddress"",
                        ""streetAddress"": ""{contact?.StreetAddress}"",
                        ""addressLocality"": ""{contact?.AddressLocality}"",
                        ""addressRegion"": ""{contact?.AddressRegion}"",
                        ""postalCode"": ""{contact?.PostalCode}"",
                        ""addressCountry"": ""{contact?.AddressCountry}""
                      }},
                      ""openingHoursSpecification"": [
                        {workingHoursJson}
                      ],
                      ""sameAs"": [
                        ""{site?.LinkedInUrl}"",
                        ""{site?.TwitterUrl}"",
                        ""{site?.FacebookUrl}"",
                        ""{site?.InstagramUrl}""
                      ]
                    }}
                    </script>";
        }

    }

}
