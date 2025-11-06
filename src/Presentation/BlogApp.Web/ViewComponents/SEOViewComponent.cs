using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.DTOs.SiteSettings;
using BlogApp.Application.Features.ContactInfo.Queries.Commands;
using BlogApp.Application.Features.ServiceCategories.Queries.GetHomePageServiceCategories;
using BlogApp.Application.Features.SiteSettings.Queries.GetSiteSettings;
using BlogApp.Web.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Caching.Memory;
using static System.Collections.Specialized.BitVector32;

namespace BlogApp.Web.ViewComponents
{
    public class SEOViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;
        private const string CACHE_KEY = "LayoutData";
        private static readonly TimeSpan CacheDuration = TimeSpan.FromHours(1);

        public SEOViewComponent(IMediator mediator, IMemoryCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<IViewComponentResult> InvokeAsync(string section, string? _title = null,
            string? _description = null, string? _keywords = null)
        {
            // Aynı cache mantığını kullan (SiteInfoViewComponent ile aynı)
            if (!_cache.TryGetValue(CACHE_KEY, out LayoutViewModel? layoutData))
            {
                layoutData = new LayoutViewModel();

                var siteSettingsResult = await _mediator.Send(new GetSiteSettingsQuery());
                if (siteSettingsResult.IsSuccess && siteSettingsResult.Data != null)
                    layoutData.SiteSettings = siteSettingsResult.Data;

                var contactInfoResult = await _mediator.Send(new GetContactInfoQuery());
                if (contactInfoResult.IsSuccess && contactInfoResult.Data != null)
                    layoutData.ContactInfo = contactInfoResult.Data;

                var servicesResult = await _mediator.Send(new GetHomePageServiceCategoriesQuery());
                if (servicesResult.IsSuccess && servicesResult.Data != null)
                    layoutData.Services = servicesResult.Data;

                _cache.Set(CACHE_KEY, layoutData, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = CacheDuration,
                    SlidingExpiration = TimeSpan.FromMinutes(30)
                });
            }

            // JSON-LD oluştur
            var jsonLd = BuildJsonLd(layoutData?.SiteSettings, layoutData?.ContactInfo);

            // Sadece HTML string olarak döndür
            return section switch
            {
                "Meta" => new HtmlContentViewComponentResult(new HtmlString(BuildMetaTags(_title, _description, _keywords,
                    layoutData?.SiteSettings?.SiteName, layoutData?.SiteSettings?.LogoUrl, layoutData?.SiteSettings?.Favicons))),
                "JsonLd" => new HtmlContentViewComponentResult(new HtmlString(jsonLd)),
                _ => Content("") // Boş içerik döndürme
            };
        }

        private string BuildJsonLd(SiteSettingsDto? site, ContactInfoDto? contact)
        {
            if (site == null || contact == null)
            {
                return string.Empty;
            }

            // Geçerli sayfa bilgileri
            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var currentPath = request.Path.Value?.TrimEnd('/') ?? "/";
            var currentUrl = $"{baseUrl}{currentPath}";

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

            var sameAsJson = site != null ?
                string.Join(",", new[]
                {
                    site.LinkedInUrl,
                    site.TwitterUrl,
                    site.FacebookUrl,
                    site.InstagramUrl
                }
                .Where(url => !string.IsNullOrEmpty(url))
                .Select(url => $@"""{url}""")) :
                string.Empty;



            // Breadcrumb otomatik üretimi
            // Path'e göre dinamik liste oluşturur (örn: /blog/detail => Ana Sayfa > Blog > Detail)
            var pathSegments = currentPath.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var breadcrumbItems = new List<string>
            {
                $@"{{
                    ""@type"": ""ListItem"",
                    ""position"": 1,
                    ""name"": ""Ana Sayfa"",
                    ""item"": ""{baseUrl}""
                }}"
            };

            for (int i = 0; i < pathSegments.Length; i++)
            {
                var segment = pathSegments[i];
                var name = segment switch
                {
                    "hakkimizda" => "Hakkımızda",
                    "hizmetler" => "Hizmetlerimiz",
                    "blog" => "Blog",
                    "contact" or "iletisim" => "İletişim",
                    _ => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(segment.Replace("-", " "))
                };

                var itemUrl = $"{baseUrl}/{string.Join("/", pathSegments.Take(i + 1))}";
                breadcrumbItems.Add($@"{{
            ""@type"": ""ListItem"",
            ""position"": {i + 2},
            ""name"": ""{name}"",
            ""item"": ""{itemUrl}""
        }}");
            }

            double? latitude = null;
            double? longitude = null;

            if (contact?.Latitude != null && contact?.Longitude != null)
            {
                latitude = contact.Latitude;
                longitude = contact.Longitude;
            }

            else if (!string.IsNullOrEmpty(contact?.MapEmbedUrl))
            {
                var mapUrl = contact.MapEmbedUrl;
                var lonMatch = System.Text.RegularExpressions.Regex.Match(mapUrl, @"!2d([0-9\.\-]+)");
                var latMatch = System.Text.RegularExpressions.Regex.Match(mapUrl, @"!3d([0-9\.\-]+)");

                if (latMatch.Success && double.TryParse(latMatch.Groups[1].Value, System.Globalization.CultureInfo.InvariantCulture, out var lat))
                {
                    latitude = lat;
                }

                if (lonMatch.Success && double.TryParse(lonMatch.Groups[1].Value, System.Globalization.CultureInfo.InvariantCulture, out var lon))
                {
                    longitude = lon;
                }
            }

            var geoSection = "";
            if (latitude.HasValue && longitude.HasValue)
            {
                geoSection = $@"
                ""geo"": {{
                  ""@type"": ""GeoCoordinates"",
                  ""latitude"": {latitude.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)},
                  ""longitude"": {longitude.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)}
                }},";
            }

            var breadcrumbJson = string.Join(",", breadcrumbItems);

            // Nihai JSON-LD çıktısı
            return $@"
                    <script type='application/ld+json'>
                    {{
                      ""@context"": ""https://schema.org"",
                      ""@graph"": [
                        {{
                          ""@type"": ""Organization"",
                          ""name"": ""{site?.SiteName}"",
                          ""url"": ""{baseUrl}"",
                          ""logo"": ""{site?.LogoUrl ?? $"{baseUrl}/images/logo.png"}"",
                          ""sameAs"": [{sameAsJson}]
                        }},
                        {{
                          ""@type"": ""LegalService"",
                          ""name"": ""{site?.SiteName}"",
                          ""image"": ""{site?.LogoUrl ?? $"{baseUrl}/images/logo.png"}"",
                          ""url"": ""{baseUrl}"",
                          ""telephone"": ""{contact?.Phone}"",
                          ""email"": ""{contact?.Email}"",
                          ""priceRange"": ""$$"",
                          ""areaServed"": ""TR"",{geoSection}
                          ""address"": {{
                            ""@type"": ""PostalAddress"",
                            ""streetAddress"": ""{contact?.StreetAddress}"",
                            ""addressLocality"": ""{contact?.AddressLocality}"",
                            ""addressRegion"": ""{contact?.AddressRegion}"",
                            ""postalCode"": ""{contact?.PostalCode}"",
                            ""addressCountry"": ""{contact?.AddressCountry}""
                          }},
                          ""openingHoursSpecification"": [{workingHoursJson}]
                        }},
                        {{
                          ""@type"": ""BreadcrumbList"",
                          ""name"": ""Breadcrumbs"",
                          ""itemListElement"": [{breadcrumbJson}]
                        }}
                      ]
                    }}
                    </script>";


        }

        private string BuildMetaTags(string? title, string? description, string? keywords,
                                                string? siteName, string? imageUrl, FaviconSetDto? favicons = null)
        {
            var sb = new System.Text.StringBuilder();
            // Production URL kontrolü
            var currentUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}";

            // 🔹 Favicons
            if (favicons != null)
            {
                if (!string.IsNullOrEmpty(favicons.Favicon16))
                    sb.AppendLine($@"<link rel=""icon"" type=""image/png"" sizes=""16x16"" href=""{favicons.Favicon16}"" />");
                if (!string.IsNullOrEmpty(favicons.Favicon32))
                    sb.AppendLine($@"<link rel=""icon"" type=""image/png"" sizes=""32x32"" href=""{favicons.Favicon32}"" />");
                if (!string.IsNullOrEmpty(favicons.Favicon180))
                    sb.AppendLine($@"<link rel=""apple-touch-icon"" sizes=""180x180"" href=""{favicons.Favicon180}"" />");
                if (!string.IsNullOrEmpty(favicons.Favicon192))
                    sb.AppendLine($@"<link rel=""icon"" type=""image/png"" sizes=""192x192"" href=""{favicons.Favicon192}"" />");
                if (!string.IsNullOrEmpty(favicons.Favicon512))
                    sb.AppendLine($@"<link rel=""icon"" type=""image/png"" sizes=""512x512"" href=""{favicons.Favicon512}"" />");
            }

            var ogDescription = string.IsNullOrEmpty(ViewData["ogDescription"] as string) ? description : ViewData["ogDescription"]!.ToString();

            // Meta Description
            if (!string.IsNullOrEmpty(ogDescription))
            {
                sb.AppendLine($@"<meta property=""og:description"" content=""{System.Net.WebUtility.HtmlEncode(ogDescription)}"" />");
                sb.AppendLine($@"<meta name=""twitter:description"" content=""{System.Net.WebUtility.HtmlEncode(ogDescription)}"" />");
            }

            var ogType = string.IsNullOrEmpty(ViewData["ogType"] as string) ? "website" : ViewData["ogType"]?.ToString();
            // Open Graph
            sb.AppendLine($@"<meta property=""og:type"" content=""{ogType}"" />");

            if (!string.IsNullOrEmpty(title)) sb.AppendLine($@"<meta property=""og:title"" content=""{System.Net.WebUtility.HtmlEncode(title)}"" />");

            sb.AppendLine($@"<meta property=""og:url"" content=""{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.Path}{HttpContext.Request.QueryString}"" />");

            if (!string.IsNullOrEmpty(siteName)) sb.AppendLine($@"<meta property=""og:site_name"" content=""{System.Net.WebUtility.HtmlEncode(siteName)}"" />");


            var ogImage = string.IsNullOrEmpty(ViewData["ogImage"] as string) ? imageUrl : ViewData["ogImage"]?.ToString();


            var absoluteImageUrl = ogImage;
            if (!string.IsNullOrEmpty(absoluteImageUrl) && absoluteImageUrl.StartsWith("/"))
            {
                absoluteImageUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{absoluteImageUrl}";
            }

            if (!string.IsNullOrEmpty(absoluteImageUrl))
            {
                sb.AppendLine($@"<meta property=""og:image"" content=""{System.Net.WebUtility.HtmlEncode(absoluteImageUrl)}"" />");
                sb.AppendLine($@"<meta property=""og:image:width"" content=""1200"" />");
                sb.AppendLine($@"<meta property=""og:image:height"" content=""630"" />");

                sb.AppendLine($@"<meta name=""twitter:image"" content=""{System.Net.WebUtility.HtmlEncode(absoluteImageUrl)}"" />");
                if (!string.IsNullOrEmpty(title)) sb.AppendLine($@"<meta property=""og:image:alt"" content=""{System.Net.WebUtility.HtmlEncode(title)}"" />");

            }


            sb.AppendLine($@"<meta property=""og:locale"" content=""tr_TR"" />");

            // Twitter Card

            sb.AppendLine($@"<meta name=""twitter:card"" content=""summary_large_image"" />");

            if (!string.IsNullOrEmpty(title)) sb.AppendLine($@"<meta name=""twitter:title"" content=""{System.Net.WebUtility.HtmlEncode(title)}"" />");

            sb.AppendLine($@"<link rel=""canonical"" href=""{currentUrl}"" />");

            return sb.ToString();
        }

    }

}
