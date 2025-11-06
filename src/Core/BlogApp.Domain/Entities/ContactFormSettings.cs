using BlogApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Domain.Entities
{
    public class ContactFormSettings : BaseEntity
    {
        // Section key (optional, useful if you keep many settings)
        public string SectionKey { get; set; } = "contact_form";

        // Header / intro
        public string SmallBadgeText { get; set; } = "Ücretsiz Danışmanlık"; // örn: badge
        public string Title { get; set; } = "Hukuki Sürecinizi Başlatın";
        public string SubtitleTemplate { get; set; } = "Bu konusunda uzman avukatlarımızdan ücretsiz ön görüşme alın"; // Template içinde değişken kullanabilirsin

        // Field Labels & Placeholders
        public string LabelServiceCategory { get; set; } = "Danışmanlık Konusu / Hukuki Alan";
        public string LabelServiceCategoryDescription { get; set; } = "Bu sayfadaki danışmanlık konusu otomatik olarak seçilmiştir";
        public string LabelFullName { get; set; } = "Ad Soyad";
        public string PlaceholderFullName { get; set; } = "Adınız ve soyadınız";

        public string LabelEmail { get; set; } = "E-posta";
        public string PlaceholderEmail { get; set; } = "ornek@email.com";

        public string LabelPhone { get; set; } = "Telefon";
        public string PlaceholderPhoneDisplay { get; set; } = "5XX XXX XX XX";
        public string PhonePrefixDisplay { get; set; } = "+90"; // gösterim için

        public string LabelSubject { get; set; } = "Konu";
        public string PlaceholderSubject { get; set; } = "Konunuzu kısaca belirtin";

        public string LabelMessage { get; set; } = "Mesajınız";
        public string PlaceholderMessage { get; set; } = "Durumunuzu detaylı bir şekilde açıklayın...";

        // KVKK
        public string KvkkTextTemplate { get; set; } = "KVKK Aydınlatma Metni’ni okudum, anladım ve kişisel verilerimin işlenmesine onay veriyorum.";
        public string KvkkLink { get; set; } = "/kvkk";

        // Submit button
        public string SubmitButtonText { get; set; } = "Ücretsiz Danışmanlık Al";


        // Validation / messages (optional)
        public bool RequireKvkkConsent { get; set; } = true;

        // Toggle
        public bool IsActive { get; set; } = true;

        // SEO
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
    }
}
