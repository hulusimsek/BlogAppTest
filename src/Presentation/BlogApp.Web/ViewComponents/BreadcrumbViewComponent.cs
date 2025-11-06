using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BlogApp.Web.ViewComponents
{
    public class BreadcrumbViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}";
            var path = request.Path.Value?.Trim('/') ?? string.Empty;

            var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            var breadcrumbs = new List<(string Name, string Url)>();

            // Ana sayfa
            breadcrumbs.Add(("Ana Sayfa", baseUrl));

            // Dinamik segmentler
            for (int i = 0; i < segments.Length; i++)
            {
                var segment = segments[i];

                string name;

                // Blog detay sayfasında isek:
                if (i >= 1 && (segments[i-1].Equals("blog", StringComparison.OrdinalIgnoreCase) ||
                    segments[i - 1].Equals("hizmetler", StringComparison.OrdinalIgnoreCase)))
                {
                    // Controller'dan gelen başlık varsa onu kullan
                    name = ViewData["BreadcrumbTitle"]?.ToString()
                        ?? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(segment.Replace("-", " "));
                }
                else
                {
                    name = segment.ToLowerInvariant() switch
                    {
                        "hakkimizda" => "Hakkımızda",
                        "hizmetler" => "Hizmetlerimiz",
                        "blog" => "Blog",
                        "iletisim" or "contact" => "İletişim",
                        "online-danismanlik" => "Online Danışmanlık",
                        _ => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(segment.Replace("-", " "))
                    };
                }

                // URL birikimli şekilde inşa edilir
                var segmentUrl = $"{baseUrl}/{string.Join("/", segments.Take(i + 1))}";

                // Türkçe isimlendirme


                breadcrumbs.Add((name, segmentUrl));
            }

            return View("Default", breadcrumbs);
        }
    }
}
