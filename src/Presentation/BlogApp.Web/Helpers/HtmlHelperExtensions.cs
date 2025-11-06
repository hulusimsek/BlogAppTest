using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using System.Web;

namespace BlogApp.Web.Helpers
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// View içinde HTML veya HTML entity içeren metni temizleyip güvenli biçimde render eder.
        /// </summary>
        public static IHtmlContent SafePlainText(this IHtmlHelper html, string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new HtmlString(string.Empty);

            var text = input;

            // HTML etiketlerini kaldır
            text = Regex.Replace(text, "<.*?>", string.Empty);

            // HTML entity'lerini decode et (&uuml; -> ü)
            text = HttpUtility.HtmlDecode(text);

            // Satır sonlarını temizle
            text = text.Replace("\r", " ").Replace("\n", " ").Replace("\t", " ");

            // Fazla boşlukları sadeleştir
            text = Regex.Replace(text, @"\s+", " ").Trim();

            // Çok uzunsa kes
            if (text.Length > 1500)
                text = text.Substring(0, 1500);

            return new HtmlString(text);
        }
    }
}
