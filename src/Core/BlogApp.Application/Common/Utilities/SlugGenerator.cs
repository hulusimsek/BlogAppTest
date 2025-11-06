using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace BlogApp.Application.Common.Utilities
{
    public static class SlugGenerator
    {
        private static readonly Regex _invalidChars = new(@"[^a-z0-9\s-]", RegexOptions.Compiled);
        private static readonly Regex _spaces = new(@"\s+", RegexOptions.Compiled);
        private static readonly Regex _dashes = new(@"-+", RegexOptions.Compiled);

        public static string GenerateSlug(string phrase, int maxLength = 100)
        {
            if (string.IsNullOrWhiteSpace(phrase))
                return string.Empty;

            string str = phrase.Trim().ToLowerInvariant();
            str = Transliterate(str);
            str = RemoveDiacritics(str);
            str = _invalidChars.Replace(str, "");
            str = _spaces.Replace(str, "-");
            str = _dashes.Replace(str, "-").Trim('-');

            if (str.Length > maxLength)
                str = str[..maxLength].Trim('-');

            return HttpUtility.UrlEncode(str);
        }

        private static string RemoveDiacritics(string text)
        {
            var normalized = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var ch in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                    sb.Append(ch);
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        // 🌍 Basit ama genişletilebilir transliteration
        private static string Transliterate(string str)
        {
            var map = new Dictionary<char, string>
            {
                // Türkçe
                ['ç'] = "c",
                ['ğ'] = "g",
                ['ı'] = "i",
                ['ö'] = "o",
                ['ş'] = "s",
                ['ü'] = "u",
                // Almanca / Fransızca / İspanyolca
                ['ß'] = "ss",
                ['ñ'] = "n",
                ['é'] = "e",
                ['è'] = "e",
                ['à'] = "a",
                // Yunanca örnekleri (isteğe göre genişletilebilir)
                ['α'] = "a",
                ['β'] = "b",
                ['γ'] = "g",
                ['δ'] = "d"
            };

            var sb = new StringBuilder(str.Length);
            foreach (var ch in str)
                sb.Append(map.TryGetValue(ch, out var r) ? r : ch);

            return sb.ToString();
        }

        // 🧩 Opsiyonel: Duplicate slug varsa slug-2 üret
        public static string EnsureUnique(string slug, Func<string, bool> exists)
        {
            var newSlug = slug;
            int counter = 2;
            while (exists(newSlug))
                newSlug = $"{slug}-{counter++}";
            return newSlug;
        }
    }

}
