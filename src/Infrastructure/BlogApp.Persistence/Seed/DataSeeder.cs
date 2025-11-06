using BlogApp.Domain.Entities;
using BlogApp.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApp.Persistence.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {


            // Eğer zaten bir site ayarı varsa, yeniden ekleme
            if (!await context.SiteSettings.AnyAsync())
            {
                var defaultSettings = new SiteSettings
                {
                    SiteName = "Avukat Elif Eylül",
                    LogoUrl = "/images/logo.png",
                    FooterText = "Tüm hakları saklıdır.",
                    CopyrightText = "© F2025 Avukat Elif Eylül",
                    TwitterUrl = "https://x.com/deneme",
                    LinkedInUrl = "https://www.linkedin.com/in/deneme",
                    FacebookUrl = "https://www.facebook.com/deneme",
                    InstagramUrl = "https://www.instagram.com/deneme",
                    ButtonText = "Randevu Al",
                    ButtonLink = "/online-danismanlik",
                    ButtonAltText = "Randevu almak için tıklayın",
                    IsActive = true,

                    // 🔹 Varsayılan favicon seti
                    Favicons = new FaviconSet
                    {
                        Favicon16 = "/favicons/favicon-16x16.png",
                        Favicon32 = "/favicons/favicon-32x32.png",
                        Favicon180 = "/favicons/favicon-180x180.png",
                        Favicon192 = "/favicons/favicon-192x192.png",
                        Favicon512 = "/favicons/favicon-512x512.png"
                    }
                };
                await context.SiteSettings.AddAsync(defaultSettings);
            }

            // Günleri kontrol ederek ekleme
            if (!await context.PageSection.AnyAsync(x => x.SectionKey == "home"))
            {
                var defaultHomePageSection = new PageSection
                {
                    SectionKey = "home",
                    Title = "Hukuki İhtiyaçlarınız İçin Güvenilir Çözümler",
                    Subtitle = "Av. Elif Eylül - Alanında uzman kadromuzla yanınızdayız.",
                    ButtonText = "Ücretsiz Ön Görüşme İçin Bize Ulaşın",
                    ButtonLink = "/iletişim",
                    BackgroundImageUrl = "/images/mainBanner.png",
                    BackgroundAltText = "Hukuk kitaplarıyla dolu, modern bir ofis ortamı.",
                    Content = "Büromuz, müvekkillerimize en etkili ve hızlı hukuki çözümleri sunma misyonuyla hareket eder. Uzmanlık alanlarımızda derinlemesine bilgi ve deneyimimizle, haklarınızı korumak ve adaletin tecellisini sağlamak için çalışıyoruz. Müvekkil odaklı yaklaşımımızla her davaya özel stratejiler geliştiriyoruz.",
                    ButtonIsActive = true,
                    IsActive = true,
                    ViewCount = 0,
                    MetaTitle = "Ana Sayfa - Avukat Elif Eylül",
                    MetaDescription = "Hukuk Bürosu Elif Eylül'in resmi sitesi.",
                    MetaKeywords = "avukat, hukuk, danışmanlık, Elif Eylül"
                };


                await context.PageSection.AddAsync(defaultHomePageSection);
            }

            if (!await context.PageSection.AnyAsync(x => x.SectionKey == "services"))
            {
                var defaultHomePageSection = new PageSection
                {
                    SectionKey = "services",
                    Title = "Hukuki Çözüm Ortağınız",
                    TitleExplanation = "Sunduğumuz uzmanlık alanlarını keşfedin.",
                    Subtitle = "Uzmanlık Alanlarımız",
                    Content = "Müvekkillerimize en üst düzeyde hukuki danışmanlık ve avukatlık hizmeti sunmak için çeşitli uzmanlık alanlarında faaliyet gösteriyoruz. Her biri kendi alanında uzman avukatlarımızla, hukuki ihtiyaçlarınıza özel çözümler üretiyoruz.",
                    AlternativeTitle = "Ücretsiz Danışmanlık İçin Randevu Alın",
                    AlternativeTitleExplanation = "Davanız hakkında bir uzmanla görüşmek ve hukuki ihtiyaçlarınız için en doğru adımı atmak üzere bizimle iletişime geçin.",
                    ButtonText = "İletişime Geçin",
                    ButtonLink = "/iletisim",
                    ButtonIsActive = true,
                    IsActive = true,
                    ViewCount = 0,
                    MetaTitle = "Hizmetlerimiz - Avukat Elif Eylül",
                    MetaDescription = "Hukuk Bürosu Elif Eylül tarafından sunulan Ceza, Aile, Ticaret, Bilişim, Miras ve diğer hukuki hizmetleri keşfedin. Uzman avukatlarla güvenli ve profesyonel çözümler.",
                    MetaKeywords = "Hukuk Bürosu, Avukat Elif Eylül, Ceza Hukuku, Aile Hukuku, Ticaret Hukuku, Bilişim Hukuku, Miras Hukuku, Hukuki Danışmanlık, İş Hukuku, Gayrimenkul Hukuku"
                };


                await context.PageSection.AddAsync(defaultHomePageSection);
            }

            if (!await context.PageSection.AnyAsync(x => x.SectionKey == "serviceDetail"))
            {
                var defaultServiceDetailPageSection = new PageSection
                {
                    SectionKey = "serviceDetail",
                    Subtitle = "Başarı Hikayelerimiz | Avukat Elif Eylül",
                    AlternativeTitle = "Süreci Akış Şeması",
                    ButtonText = "Ücretsiz Danışmanlık Alın",
                    ButtonAltText = "Bize ulaşarak ücretsiz ön görüşme almak için bu butona tıklayın",
                    ButtonLink = "/iletişim",
                    ButtonIsActive = true,
                    IsActive = true,
                };

                await context.PageSection.AddAsync(defaultServiceDetailPageSection);
            }

            if (!await context.PageSection.AnyAsync(x => x.SectionKey == "blogs"))
            {
                var defaultServiceDetailPageSection = new PageSection
                {
                    SectionKey = "blogs",
                    Title = "Hukuki Makalelerimiz",
                    MetaTitle = "Blog | Avukat Elif Eylül - Güncel Hukuki Makaleler",
                    MetaDescription = "Ceza, aile, ticaret ve bilişim hukuku üzerine uzman avukatlarımızdan güncel makaleler. 19 makale ile hukuki bilgilerinizi geliştirin.",
                    ButtonAltText = "yazı bulundu",
                    AlternativeTitle = "Etiketli Yazılar",
                    AlternativeTitleExplanation = "Makaleleri",
                    ButtonIsActive = false,
                    IsActive = true,
                };


                await context.PageSection.AddAsync(defaultServiceDetailPageSection);
            }

            if (!await context.PageSection.AnyAsync(x => x.SectionKey == "contactInfo"))
            {
                var defaultServiceDetailPageSection = new PageSection
                {
                    SectionKey = "contactInfo",
                    MetaTitle = "İletişim | Kadıköy Avukat | Avukat Elif Eylül",
                    MetaDescription = "Kadıköy İstanbul avukat Avukat Elif Eylül. Randevu için: +90 212 123 45 67. Ücretsiz ön görüşme.",
                    Title = "Kadıköy Avukat İletişim | Avukat Elif Eylül",
                    TitleExplanation = "Sorularınız, talepleriniz veya randevu almak için aşağıdaki bilgilerden bize ulaşabilirsiniz.",
                    Subtitle = "Bize Ulaşın ve Destek Alın",
                    ButtonText = "Bize Ulaşın",
                    ButtonAltText = "Randevu veya soru sormak için bize ulaşın.",
                    ButtonIsActive = true,
                    IsActive = true,
                };


                await context.PageSection.AddAsync(defaultServiceDetailPageSection);
            }

            if (!await context.PageSection.AnyAsync(x => x.SectionKey == "tags"))
            {
                var defaultServiceDetailPageSection = new PageSection
                {
                    SectionKey = "tags",
                    Title = "Blog Etiketleri - Hukuk Konuları",
                    TitleExplanation = "Bu sayfada blog yazılarımızda en sık kullanılan etiketleri bulabilirsiniz. Ceza hukuku, boşanma, iş hukuku gibi konularda yazılarımıza bu etiketler üzerinden kolayca ulaşabilirsiniz.",
                    MetaTitle = "Etiketler - Hukuk Avukat Elif Eylül",
                    ButtonAltText = "etiket bulundu",
                    MetaDescription = "Avukat Elif Eylül'ün blog yazılarında ele alınan hukuki konular: İş hukuku, KVKK, icra iflas, dijital güvenlik, miras hukuku ve daha fazlası. Etiketlere göz atın.",
                    ButtonIsActive = false,
                    IsActive = true,
                };


                await context.PageSection.AddAsync(defaultServiceDetailPageSection);
            }

            if (!await context.PageSection.AnyAsync(x => x.SectionKey == "categories"))
            {
                var defaultServiceDetailPageSection = new PageSection
                {
                    SectionKey = "categories",
                    Title = "Blog Kategorileri - Hukuk Konuları",
                    TitleExplanation = "Bu sayfada blog yazılarımızın kategorilerini bulabilirsiniz. Ceza hukuku, boşanma, iş hukuku gibi farklı konularda yazılarımıza bu kategoriler üzerinden kolayca ulaşabilirsiniz.",
                    ButtonAltText = "kategori bulundu",
                    MetaTitle = "Tüm Blog Kategorileri | Elif Eylül Hukuk Blogu",
                    MetaDescription = "Elif Eylül Hukuk Blogu'nda yer alan tüm yazı kategorilerini keşfedin. İcra, iflas, aile hukuku ve daha fazlası.",
                    ButtonIsActive = false,
                    IsActive = true,
                };


                await context.PageSection.AddAsync(defaultServiceDetailPageSection);
            }

            if (!await context.ContactFormSettings.AnyAsync(x => x.SectionKey == "serviceDetail"))
            {
                var defaultSettings = new ContactFormSettings
                {
                    SectionKey = "serviceDetail",

                    SmallBadgeText = "Ücretsiz Danışmanlık",
                    Title = "Hukuki Sürecinizi Başlatın",
                    SubtitleTemplate = "Bu konuda uzman avukatlarımızdan ücretsiz ön görüşme alın",

                    // Field Labels & Placeholders
                    LabelServiceCategory = "Danışmanlık Konusu / Hukuki Alan",
                    LabelServiceCategoryDescription = "Bu sayfadaki danışmanlık konusu otomatik olarak seçilmiştir",
                    LabelFullName = "Ad Soyad",
                    PlaceholderFullName = "Adınız ve soyadınız",

                    LabelEmail = "E-posta",
                    PlaceholderEmail = "ornek@email.com",

                    LabelPhone = "Telefon",
                    PlaceholderPhoneDisplay = "5XX XXX XX XX",
                    PhonePrefixDisplay = "+90",

                    LabelSubject = "Konu",
                    PlaceholderSubject = "Konunuzu kısaca belirtin",

                    LabelMessage = "Mesajınız",
                    PlaceholderMessage = "Durumunuzu detaylı bir şekilde açıklayın...",

                    // KVKK
                    KvkkTextTemplate = "KVKK Aydınlatma Metni’ni okudum, anladım ve kişisel verilerimin işlenmesine onay veriyorum.",
                    KvkkLink = "/kvkk",

                    // Submit button
                    SubmitButtonText = "Ücretsiz Danışmanlık Al",

                    // Validation / messages (optional)
                    RequireKvkkConsent = true,

                    // Toggle
                    IsActive = true
                };
                await context.ContactFormSettings.AddAsync(defaultSettings);
            }

            if (!await context.ContactFormSettings.AnyAsync(x => x.SectionKey == "appointment"))
            {
                var defaultSettings = new ContactFormSettings
                {
                    SectionKey = "appointment",

                    SmallBadgeText = "Ücretsiz hukuki danışmanlık için randevu talep formu",
                    Title = "Online Danışmanlık ve Randevu Talep Formu",
                    SubtitleTemplate = "Randevu alabilir veya soru sorabilirsiniz.",

                    // Field Labels & Placeholders
                    LabelServiceCategory = "Danışmanlık Konusu / Hukuki Alan",
                    LabelServiceCategoryDescription = "Bir konu seçin",
                    LabelFullName = "Ad Soyad",
                    PlaceholderFullName = "Adınız ve soyadınız",

                    LabelEmail = "E-posta",
                    PlaceholderEmail = "ornek@email.com",

                    LabelPhone = "Telefon",
                    PlaceholderPhoneDisplay = "5XX XXX XX XX",
                    PhonePrefixDisplay = "+90",

                    LabelSubject = "Konu",
                    PlaceholderSubject = "Konunuzu kısaca belirtin",

                    LabelMessage = "Mesajınız",
                    PlaceholderMessage = "Durumunuzu detaylı bir şekilde açıklayın...",

                    // KVKK
                    KvkkTextTemplate = "KVKK Aydınlatma Metni’ni okudum, anladım ve kişisel verilerimin işlenmesine onay veriyorum.",
                    KvkkLink = "/kvkk",

                    // Submit button
                    SubmitButtonText = "Ücretsiz Danışmanlık Al",

                    // Validation / messages (optional)
                    RequireKvkkConsent = true,

                    // Toggle
                    IsActive = true,

                    MetaTitle = "Online Hukuki Danışmanlık ve Randevu Talebi | Avukat Elif Eylül",

                    MetaDescription = "Elif Eylül, Avukat ile ücretsiz hukuki danışmanlık almak ve online randevu talep etmek için hemen başvurun. Soru sorabilir ve uzman desteği alabilirsiniz."
                };
                await context.ContactFormSettings.AddAsync(defaultSettings);
            }


            if (!await context.ContactInfos.AnyAsync())
            {
                var contactInfo = new ContactInfo
                {
                    Address = "123 Hukuk Sokağı, Adalet Plaza, Kat: 4, Daire: 12, Istanbul",
                    Phone = "+90 212 123 45 67",
                    Email = "info@hukukburosu.com",
                    MapEmbedUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3009.5878662216996!2d28.853895676451128!3d41.03427176782329!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14caa52ceaf3d931%3A0xd6318ad36c35ebbd!2zQmHEn2PEsWxhciBNZXlkYW4!5e0!3m2!1str!2sus!4v1762305559408!5m2!1str!2sus",
                    Latitude = 41.03427176782329,
                    Longitude = 28.853895676451128,
                    IsActive = true,

                    // Yeni eklenen adres alanları
                    StreetAddress = "Hukuk Sokağı, Adalet Plaza, Kat: 4, Daire: 12",
                    AddressLocality = "Kadıköy",
                    AddressRegion = "İstanbul",
                    PostalCode = "34710",
                    AddressCountry = "TR",

                    // Buraya doğrudan FAQ verilerini ekleyebiliriz 👇
                    Faqs = new List<ContactInfoFaqItem>
                    {
                        new ContactInfoFaqItem
                        {
                            Question = "Randevu nasıl alabilirim?",
                            Answer = "Randevu almak için bizi arayabilir veya web sitemizdeki iletişim formunu doldurabilirsiniz."
                        },
                        new ContactInfoFaqItem
                        {
                            Question = "Çalışma saatleriniz nedir?",
                            Answer = "Hafta içi her gün 09:00 - 18:00 saatleri arasında hizmet vermekteyiz."
                        },
                        new ContactInfoFaqItem
                        {
                            Question = "İlk görüşme ücretsiz mi?",
                            Answer = "İlk danışmanlık görüşmesi ücretsizdir. Detaylı bilgi için bizimle iletişime geçebilirsiniz."
                        }
                    }
                };

                await context.ContactInfos.AddAsync(contactInfo);
                await context.SaveChangesAsync();
            }

            // ContactInfo kaydını çekelim
            var contact = await context.ContactInfos
                .FirstOrDefaultAsync(x => x.IsActive);

            if (contact != null)
            {
                // Günleri kontrol ederek ekleme
                if (!await context.WorkingHours.AnyAsync(x => x.DayNameEn == "Monday"))
                {
                    await context.WorkingHours.AddAsync(new WorkingHour
                    {
                        ContactInfoId = contact.Id,
                        DayNameTr = "Pazartesi",
                        DayNameEn = "Monday",
                        Opens = "09:00",
                        Closes = "18:00",
                        IsClosed = false
                    });
                }

                if (!await context.WorkingHours.AnyAsync(x => x.DayNameEn == "Tuesday"))
                {
                    await context.WorkingHours.AddAsync(new WorkingHour
                    {
                        ContactInfoId = contact.Id,
                        DayNameTr = "Salı",
                        DayNameEn = "Tuesday",
                        Opens = "09:00",
                        Closes = "18:00",
                        IsClosed = false
                    });
                }

                if (!await context.WorkingHours.AnyAsync(x => x.DayNameEn == "Wednesday"))
                {
                    await context.WorkingHours.AddAsync(new WorkingHour
                    {
                        ContactInfoId = contact.Id,
                        DayNameTr = "Çarşamba",
                        DayNameEn = "Wednesday",
                        Opens = "09:00",
                        Closes = "18:00",
                        IsClosed = false
                    });
                }

                if (!await context.WorkingHours.AnyAsync(x => x.DayNameEn == "Thursday"))
                {
                    await context.WorkingHours.AddAsync(new WorkingHour
                    {
                        ContactInfoId = contact.Id,
                        DayNameTr = "Perşembe",
                        DayNameEn = "Thursday",
                        Opens = "09:00",
                        Closes = "18:00",
                        IsClosed = false
                    });
                }

                if (!await context.WorkingHours.AnyAsync(x => x.DayNameEn == "Friday"))
                {
                    await context.WorkingHours.AddAsync(new WorkingHour
                    {
                        ContactInfoId = contact.Id,
                        DayNameTr = "Cuma",
                        DayNameEn = "Friday",
                        Opens = "09:00",
                        Closes = "18:00",
                        IsClosed = false
                    });
                }

                if (!await context.WorkingHours.AnyAsync(x => x.DayNameEn == "Saturday"))
                {
                    await context.WorkingHours.AddAsync(new WorkingHour
                    {
                        ContactInfoId = contact.Id,
                        DayNameTr = "Cumartesi",
                        DayNameEn = "Saturday",
                        Opens = "10:00",
                        Closes = "14:00",
                        IsClosed = false
                    });
                }

                if (!await context.WorkingHours.AnyAsync(x => x.DayNameEn == "Sunday"))
                {
                    await context.WorkingHours.AddAsync(new WorkingHour
                    {
                        ContactInfoId = contact.Id,
                        DayNameTr = "Pazar",
                        DayNameEn = "Sunday",
                        Opens = "",
                        Closes = "",
                        IsClosed = true
                    });
                }
            }





            if (!await context.ServiceCategories.AnyAsync())
            {
                var serviceCategories = new List<ServiceCategory>
                {
                    new ServiceCategory
                    {
                        Name = "Ceza Hukuku",
                        Slug = "ceza-hukuku",
                        IconName = "icons/services/gavel.png",
                        ShortDescription = "Suç ve ceza davalarında şüpheli, sanık veya mağdur taraf vekilliği.",
                        DisplayOrder = 1,
                        IsActive = true,
                        ShowOnHomePage = true,
                        ServiceDetail = new ServiceDetail
                        {
                            Subtitle = "deneme alt başlık",
                            ImageUrl = "/images/services/ceza-hukuku.jpg",
                            DetailedDescription = "<p>Ceza hukuku, suç ve ceza davalarında tarafları temsil etmek.</p>", // HTML destekli açıklama
                            ProcessFlowSteps = new List<ProcessFlowStep>
                            {
                                new ProcessFlowStep { StepNumber = 1, Title = "İlk Danışmanlık", Description = "Danışmanlık hizmeti alınıp, dava süreci anlatılır.", IconName = "icons/process/consult.png" },
                                new ProcessFlowStep { StepNumber = 2, Title = "Savunma Hazırlığı", Description = "Savunma için gerekli belgeler toplanır.", IconName = "icons/process/defense.png" },
                                new ProcessFlowStep { StepNumber = 3, Title = "Mahkeme Süreci", Description = "Mahkemeye katılım ve savunma yapılır.", IconName = "icons/process/court.png" }
                            },
                            Faqs = new List<ServiceFaqItem>
                            {
                                new ServiceFaqItem { Question = "Ceza davasında nasıl savunma yapabilirim?", Answer = "Savunma avukatınız tarafından yapılacaktır." },
                                new ServiceFaqItem { Question = "Dava süresi ne kadar sürer?", Answer = "Dava süresi, davanın türüne göre değişir." }
                            },
                            Testimonials = new List<TestimonialItem>
                            {
                                new TestimonialItem { Content = "Ceza davama yardımcı oldukları için çok teşekkür ederim!", AuthorName = "Ahmet Y.", AuthorLocation = "İstanbul" },
                                new TestimonialItem { Content = "Çok profesyonel bir ekip, her şey çok hızlı ilerledi.", AuthorName = "Murat S.", AuthorLocation = "Ankara" },
                                new TestimonialItem { Content = "Ceza davama yardımcı oldukları için çok teşekkür ederim!", AuthorName = "Ahmet Y.", AuthorLocation = "İstanbul" },
                                new TestimonialItem { Content = "Çok profesyonel bir ekip, her şey çok hızlı ilerledi.", AuthorName = "Murat S.", AuthorLocation = "Ankara" },
                                new TestimonialItem { Content = "Ceza davama yardımcı oldukları için çok teşekkür ederim!", AuthorName = "Ahmet Y.", AuthorLocation = "İstanbul" },
                                new TestimonialItem { Content = "Çok profesyonel bir ekip, her şey çok hızlı ilerledi.", AuthorName = "Murat S.", AuthorLocation = "Ankara" }


                            }
                        }
                    },
                    new ServiceCategory
                    {
                        Name = "Aile Hukuku",
                        Slug = "aile-hukuku",
                        IconName = "icons/services/groups.png",
                        ShortDescription = "Boşanma, velayet, nafaka ve mal paylaşımı gibi ailevi konularda hukuki destek.",
                        DisplayOrder = 2,
                        IsActive = true,
                        ShowOnHomePage = true,
                        ServiceDetail = new ServiceDetail
                        {
                            ImageUrl = "/images/services/aile-hukuku.png",
                            DetailedDescription = "<p>Aile hukuku, boşanma, nafaka ve velayet davalarını içerir.</p>",
                            ProcessFlowSteps = new List<ProcessFlowStep>
                            {
                                new ProcessFlowStep { StepNumber = 1, Title = "Dava Hazırlığı", Description = "Boşanma için gerekli belgeler toplanır.", IconName = "icons/process/documents.png" },
                                new ProcessFlowStep { StepNumber = 2, Title = "Mahkeme Başvurusu", Description = "Mahkemeye başvuru yapılır.", IconName = "icons/process/lawsuit.png" },
                                new ProcessFlowStep { StepNumber = 3, Title = "Duruşmalar", Description = "Duruşmalar yapılır ve karar verilir.", IconName = "icons/process/hearing.png" }
                            },
                            Faqs = new List<ServiceFaqItem>
                            {
                                new ServiceFaqItem { Question = "Boşanma davası ne kadar sürer?", Answer = "Boşanma davası, anlaşmalı ya da çekişmeli olmasına göre değişir." },
                                new ServiceFaqItem { Question = "Velayet davası nasıl açılır?", Answer = "Velayet davası için mahkemeye başvuru yapılır." }
                            },
                            Testimonials = new List<TestimonialItem>
                            {
                                new TestimonialItem { Content = "Ailemle ilgili sorunlarımı hızla çözdüler, çok memnun kaldım.", AuthorName = "Leyla T.", AuthorLocation = "İzmir" },
                                new TestimonialItem { Content = "Boşanma sürecinde çok yardımcı oldular, süreç çok hızlı geçti.", AuthorName = "Seda K.", AuthorLocation = "Bursa" }
                            }
                        }
                    },
                    new ServiceCategory
                    {
                        Name = "Ticaret Hukuku",
                        Slug = "ticaret-hukuku",
                        IconName = "icons/services/business.png",
                        ShortDescription = "Şirket kuruluşu, sözleşmeler ve ticari uyuşmazlıkların çözümü.",
                        DisplayOrder = 3,
                        IsActive = true,
                        ShowOnHomePage = true,
                        ServiceDetail = new ServiceDetail
                        {
                            ImageUrl = "/images/services/ticaret-hukuku.jpeg",
                            DetailedDescription = "<p>Ticaret hukuku, şirket kuruluşu, ticari sözleşmeler ve ticari uyuşmazlıkları kapsar.</p>",
                            ProcessFlowSteps = new List<ProcessFlowStep>
                            {
                                new ProcessFlowStep { StepNumber = 1, Title = "Şirket Kuruluşu", Description = "Şirket kurulum işlemleri yapılır.", IconName = "icons/process/company.png" },
                                new ProcessFlowStep { StepNumber = 2, Title = "Sözleşme Hazırlığı", Description = "Ticari sözleşmeler hazırlanır.", IconName = "icons/process/contract.png" },
                                new ProcessFlowStep { StepNumber = 3, Title = "Uyuşmazlık Çözümü", Description = "Ticari uyuşmazlıklar çözülür.", IconName = "icons/process/dispute.png" }
                            },
                            Faqs = new List<ServiceFaqItem>
                            {
                                new ServiceFaqItem { Question = "Şirket kuruluşu ne kadar sürer?", Answer = "Şirket kuruluşu işlemi yaklaşık 1-2 hafta sürebilir." },
                                new ServiceFaqItem { Question = "Ticaret sözleşmesi nasıl hazırlanır?", Answer = "Sözleşme, tarafların anlaşmasına ve ihtiyaçlarına göre hazırlanır." }
                            },
                            Testimonials = new List<TestimonialItem>
                            {
                                new TestimonialItem { Content = "Şirketimizi kurmamıza yardımcı oldular, tüm süreçleri çok hızlı hallettiler.", AuthorName = "Mehmet T.", AuthorLocation = "İstanbul" },
                                new TestimonialItem { Content = "Ticari sözleşme konusunda çok profesyoneller, tavsiye ederim.", AuthorName = "Ali V.", AuthorLocation = "Ankara" }
                            }
                        }
                    },
                    new ServiceCategory
                    {
                        Name = "Bilişim Hukuku",
                        Slug = "bilisim-hukuku",
                        IconName = "icons/services/technology-law.png",
                        ShortDescription = "Siber suçlar, e-ticaret ve kişisel verilerin korunması hukuku.",
                        DisplayOrder = 4,
                        IsActive = true,
                        ShowOnHomePage = true,
                        ServiceDetail = new ServiceDetail
                        {
                            ImageUrl = "/images/services/bilisim-hukuku.png",
                            DetailedDescription = "<p>Bilişim hukuku, siber suçlar ve kişisel verilerin korunması gibi konuları kapsar.</p>",
                            ProcessFlowSteps = new List<ProcessFlowStep>
                            {
                                new ProcessFlowStep { StepNumber = 1, Title = "Siber Suçlar", Description = "Siber suçlar ile ilgili dava süreci başlatılır.", IconName = "icons/process/cybercrime.png" },
                                new ProcessFlowStep { StepNumber = 2, Title = "E-Ticaret Düzenlemeleri", Description = "E-ticaretle ilgili yasal düzenlemeler yapılır.", IconName = "icons/process/e-commerce.png" },
                                new ProcessFlowStep { StepNumber = 3, Title = "Kişisel Verilerin Korunması", Description = "Kişisel verilerin korunması için önlemler alınır.", IconName = "icons/process/privacy.png" }
                            },
                            Faqs = new List<ServiceFaqItem>
                            {
                                new ServiceFaqItem { Question = "Siber suçlar nelerdir?", Answer = "Siber suçlar, internet üzerinden yapılan suçlardır." },
                                new ServiceFaqItem { Question = "E-ticaret yasaları nedir?", Answer = "E-ticaret yasaları, online ticaretin düzenlenmesine yönelik kanunlardır." }
                            },
                            Testimonials = new List<TestimonialItem>
                            {
                                new TestimonialItem { Content = "Siber güvenlik konusunda harika bir destek aldık, çok memnun kaldık.", AuthorName = "Kemal B.", AuthorLocation = "İstanbul" },
                                new TestimonialItem { Content = "E-ticaret platformum için gerekli tüm yasal düzenlemeleri hızlıca sağladılar.", AuthorName = "Ayşe P.", AuthorLocation = "Antalya" }
                            }
                        }
                    },
                    new ServiceCategory
                    {
                        Name = "Miras Hukuku",
                        Slug = "miras-hukuku",
                        IconName = "icons/services/balance.png",
                        ShortDescription = "Mirasçılık belgesi, vasiyetname ve mirasın reddi gibi işlemler.",
                        DisplayOrder = 5,
                        IsActive = true,
                        ShowOnHomePage = true,
                        ServiceDetail = new ServiceDetail
                        {
                            ImageUrl = "/images/services/miras-hukuku.jpg",
                            DetailedDescription = "<p>Miras hukuku, mirasçılık belgesi ve vasiyetname işlemleri ile ilgilidir.</p>",
                            ProcessFlowSteps = new List<ProcessFlowStep>
                            {
                                new ProcessFlowStep { StepNumber = 1, Title = "Vasiyetname Düzenleme", Description = "Vasiyetname hazırlanır ve düzenlenir.", IconName = "icons/process/will.png" },
                                new ProcessFlowStep { StepNumber = 2, Title = "Mirasçılık Belgesi", Description = "Mirasçılık belgesi çıkarılır.", IconName = "icons/process/inheritance.png" },
                                new ProcessFlowStep { StepNumber = 3, Title = "Miras Reddi", Description = "Miras reddi işlemi yapılır.", IconName = "icons/process/rejection.png" }
                            },
                            Faqs = new List<ServiceFaqItem>
                            {
                                new ServiceFaqItem { Question = "Vasiyetname nasıl yazılır?", Answer = "Vasiyetname, mirasçıların isteklerini içerir ve noter tarafından onaylanır." },
                                new ServiceFaqItem { Question = "Miras reddi nasıl yapılır?", Answer = "Miras reddi, yasal mirasçı tarafından yapılabilir." }
                            },
                            Testimonials = new List<TestimonialItem>
                            {
                                new TestimonialItem { Content = "Miras hukuku konusunda çok profesyonel bir yaklaşım sergilediler.", AuthorName = "Emine K.", AuthorLocation = "Bolu" },
                                new TestimonialItem { Content = "Miras işlemlerini hızla çözüme kavuşturdu, çok teşekkür ederim.", AuthorName = "Fikret A.", AuthorLocation = "Bursa" }
                            }
                        }
                    },
                    new ServiceCategory
                    {
                        Name = "Gayrimenkul Hukuku",
                        Slug = "gayrimenkul-hukuku",
                        IconName = "icons/services/attorney.png",
                        ShortDescription = "Tapu işlemleri, kira sözleşmeleri ve gayrimenkul alım-satım davaları.",
                        DisplayOrder = 6,
                        IsActive = true,
                        ShowOnHomePage = true,
                        ServiceDetail = new ServiceDetail
                        {
                            ImageUrl = "/images/services/gayrimenkul-hukuku.jpg",
                            DetailedDescription = "<p>Gayrimenkul hukuku, tapu işlemleri ve alım-satım işlemleri ile ilgilidir.</p>",
                            ProcessFlowSteps = new List<ProcessFlowStep>
                            {
                                new ProcessFlowStep { StepNumber = 1, Title = "Tapu İşlemleri", Description = "Tapu işlemleri gerçekleştirilir.", IconName = "icons/process/deed.png" },
                                new ProcessFlowStep { StepNumber = 2, Title = "Kira Sözleşmesi", Description = "Kira sözleşmesi hazırlanır.", IconName = "icons/process/rental.png" },
                                new ProcessFlowStep { StepNumber = 3, Title = "Alım-Satım", Description = "Gayrimenkul alım-satımı yapılır.", IconName = "icons/process/sale.png" }
                            },
                            Faqs = new List<ServiceFaqItem>
                            {
                                new ServiceFaqItem { Question = "Tapu işlemleri nasıl yapılır?", Answer = "Tapu işlemleri noter ve tapu müdürlüklerinde yapılır." },
                                new ServiceFaqItem { Question = "Kira sözleşmesi nasıl hazırlanır?", Answer = "Kira sözleşmesi, kiracı ve mal sahibi arasında imzalanan yazılı bir belgedir." }
                            },
                            Testimonials = new List<TestimonialItem>
                            {
                                new TestimonialItem { Content = "Gayrimenkul alım satım işlemlerimi hızlıca çözüme kavuşturdu.", AuthorName = "Burak M.", AuthorLocation = "Antalya" },
                                new TestimonialItem { Content = "Kira sözleşmesi konusunda çok yardımcı oldular, çok teşekkür ederim.", AuthorName = "Fatma K.", AuthorLocation = "İstanbul" }
                            }
                        }
                    },
                    new ServiceCategory
                    {
                        Name = "İş Hukuku",
                        Slug = "is-hukuku",
                        IconName = "icons/services/work.png",
                        ShortDescription = "İşe iade, kıdem tazminatı ve iş kazası davaları.",
                        DisplayOrder = 7,
                        IsActive = true,
                        ShowOnHomePage = true,
                        ServiceDetail = new ServiceDetail
                        {
                            ImageUrl = "/images/services/is-hukuku.jpg",
                            DetailedDescription = "<p>İş hukuku, işçi hakları ve iş kazaları ile ilgilidir.</p>",
                            ProcessFlowSteps = new List<ProcessFlowStep>
                            {
                                new ProcessFlowStep { StepNumber = 1, Title = "İşe İade", Description = "İşten çıkarılan çalışan işine geri dönebilir.", IconName = "icons/process/reinstatement.png" },
                                new ProcessFlowStep { StepNumber = 2, Title = "Kıdem Tazminatı", Description = "Çalışan kıdem tazminatını alabilir.", IconName = "icons/process/severance.png" },
                                new ProcessFlowStep { StepNumber = 3, Title = "İş Kazası", Description = "İş kazası durumunda tazminat ödenir.", IconName = "icons/process/accident.png" }
                            },
                            Faqs = new List<ServiceFaqItem>
                            {
                                new ServiceFaqItem { Question = "İşe iade davası nasıl açılır?", Answer = "İşe iade davası, iş mahkemesine başvuru ile yapılır." },
                                new ServiceFaqItem { Question = "İş kazası tazminatı nasıl alınır?", Answer = "İş kazası tazminatı için başvurular İŞKUR ve mahkeme aracılığıyla yapılır." }
                            },
                            Testimonials = new List<TestimonialItem>
                            {
                                new TestimonialItem { Content = "İşe iade konusunda çok yardımcı oldular, çok profesyoneller.", AuthorName = "Hüseyin A.", AuthorLocation = "Ankara" },
                                new TestimonialItem { Content = "Kıdem tazminatımı aldım, çok memnunum.", AuthorName = "Duygu S.", AuthorLocation = "İstanbul" }
                            }
                        }
                    },
                    new ServiceCategory
                    {
                        Name = "İcra ve İflas Hukuku",
                        Slug = "icra-iflas-hukuku",
                        IconName = "icons/services/bankruptcy.png",
                        ShortDescription = "Alacak tahsili, haciz işlemleri ve iflas süreçleri.",
                        DisplayOrder = 8,
                        IsActive = true,
                        ShowOnHomePage = true,
                        ServiceDetail = new ServiceDetail
                        {
                            ImageUrl = "/images/services/icra-iflas-hukuku.jpg",
                            DetailedDescription = "<p>İcra ve iflas hukuku, alacak tahsili ve iflas işlemlerini kapsar.</p>",
                            ProcessFlowSteps = new List<ProcessFlowStep>
                            {
                                new ProcessFlowStep { StepNumber = 1, Title = "Alacak Tahsili", Description = "Alacak tahsilat işlemleri başlatılır.", IconName = "icons/process/debt.png" },
                                new ProcessFlowStep { StepNumber = 2, Title = "Haciz İşlemleri", Description = "Haciz işlemleri yapılır.", IconName = "icons/process/seizure.png" },
                                new ProcessFlowStep { StepNumber = 3, Title = "İflas İşlemleri", Description = "İflas süreci başlatılır.", IconName = "icons/process/insolvency.png" }
                            },
                            Faqs = new List<ServiceFaqItem>
                            {
                                new ServiceFaqItem { Question = "Haciz işlemleri nasıl yapılır?", Answer = "Haciz işlemleri mahkeme kararı ile yapılır." },
                                new ServiceFaqItem { Question = "İflas davası nasıl açılır?", Answer = "İflas davası, borçlunun borçlarını ödeyemediği durumlarda açılır." }
                            },
                            Testimonials = new List<TestimonialItem>
                            {
                                new TestimonialItem { Content = "İflas sürecimde yardımcı oldular, çok teşekkür ederim.", AuthorName = "Cem A.", AuthorLocation = "İstanbul" },
                                new TestimonialItem { Content = "Alacak tahsili işlemlerini çok hızlı çözüme kavuşturduk.", AuthorName = "Okan K.", AuthorLocation = "Ankara" }
                            }
                        }
                    }
                };

                await context.ServiceCategories.AddRangeAsync(serviceCategories);
                await context.SaveChangesAsync();
            }


            if (!await context.BlogPosts.AnyAsync())
            {
                var blogPosts = new List<BlogPost>
                {
                    new BlogPost
                    {
                        Title = "Bilişim Hukuku ve Siber Suçlar",
                        Slug = "bilisim-hukuku-ve-siber-suclar-4",
                        Summary = "Bilişim hukuku ve siber suçlar, internet ortamında işlenen suçları ele alır. Bu yazıda siber suçlarla ilgili yasal düzenlemeler hakkında bilgi vereceğiz.",
                        Content = "<p>Bilişim hukuku, dijital ortamda işlenen suçlar, siber saldırılar ve veri güvenliği ile ilgilidir. Türk Ceza Kanunu'nda siber suçlarla ilgili düzenlemeler bulunmaktadır...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/bilisim-suclari.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 150,
                        DisplayOrder = 1,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "bilisim-hukuku")?.Id,
                        MetaTitle = "Bilişim Hukuku ve Siber Suçlar - Avukat Elif Eylül",
                        MetaDescription = "Bilişim hukuku, siber suçlar ve veri güvenliği üzerine bilgi sağlayan kapsamlı bir rehber.",
                        MetaKeywords = "bilişim hukuku, siber suçlar, internet suçları, dijital güvenlik",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "Siber suç nedir?",
                                Answer = "Siber suç, bilgisayar sistemleri ve internet üzerinden işlenen suçlardır. Bu tür suçlar arasında hacking, kimlik hırsızlığı, dolandırıcılık ve veri ihlalleri bulunur."
                            },
                            new BlogFaqItem
                            {
                                Question = "Bilişim suçlarının cezası nedir?",
                                Answer = "Bilişim suçlarının cezaları Türk Ceza Kanunu'nda düzenlenmiştir. Bu suçlar hapis cezası ve adli para cezası ile cezalandırılabilir. Cezanın miktarı suçun niteliğine ve şiddetine göre değişir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Siber saldırı durumunda ne yapmalıyım?",
                                Answer = "Siber saldırıya uğradığınızı düşünüyorsanız, öncelikle ilgili kurumları (BT uzmanları, savcılık) bilgilendirmeli ve delilleri korumalısınız. Ayrıca bir bilişim hukuku avukatından destek almalısınız."
                            }
                        }

                    },
                    new BlogPost
                    {
                        Title = "E-Ticaret ve Hukuki Düzenlemeler",
                        Slug = "e-ticaret-ve-hukuki-duzenlemeler-4",
                        Summary = "E-ticaretin hukuki boyutları ve internet üzerinden ticaretin getirdiği yasal sorumluluklar hakkında bilmeniz gerekenler.",
                        Content = "<p>E-ticaret, son yıllarda hızla büyüyen bir sektör olmuştur. Ancak, e-ticaret yapan şirketlerin ve bireylerin de yasal sorumlulukları bulunmaktadır...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/e-ticaret.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 120,
                        DisplayOrder = 2,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "bilisim-hukuku")?.Id,
                        MetaTitle = "E-Ticaret ve Hukuki Düzenlemeler - Avukat Elif Eylül",
                        MetaDescription = "E-ticaret yapanların ve alışveriş yapanların bilmesi gereken hukuki düzenlemeler hakkında rehber.",
                        MetaKeywords = "e-ticaret, hukuk, internet, yasal düzenlemeler, dijital ticaret",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "E-ticaret sitesi açmak için hangi yasal izinler gereklidir?",
                                Answer = "E-ticaret sitesi açmak için Ticaret Sicil Gazetesi'nde ilan, vergi numarası alımı ve Mesleki Sorumluluk Sigortası yaptırma gibi yükümlülükler bulunmaktadır. Ayrıca KVKK uyumluluğu da gereklidir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Tüketici hakları e-ticarette nasıl korunuyor?",
                                Answer = "6502 sayılı Tüketicinin Korunması Hakkında Kanun, e-ticaret yoluyla yapılan alışverişlerde tüketicilere 14 gün içinde cayma hakkı, ayıplı maldan doğan haklar ve garanti hakları tanımaktadır."
                            },
                            new BlogFaqItem
                            {
                                Question = "E-ticarette mesafeli satış sözleşmesi zorunlu mu?",
                                Answer = "Evet, mesafeli satış sözleşmesi e-ticaret siteleri için zorunludur. Bu sözleşmede satıcı bilgileri, mal/hizmet bilgileri, teslimat koşulları ve cayma hakkı gibi bilgiler yer almalıdır."
                            }
                        }
                    },
                    new BlogPost
                    {
                        Title = "Kişisel Verilerin Korunması Kanunu (KVKK)",
                        Slug = "kisisel-verilerin-korunmasi-kanunu-kvkk-4",
                        Summary = "KVKK, kişisel verilerin korunması için çıkarılmış bir yasadır. Bu yazıda KVKK'nın önemi ve şirketlerin uyum sağlaması gereken noktaları anlatıyoruz.",
                        Content = "<p>KVKK, kişisel verilerin işlenmesini, saklanmasını ve güvenliğini sağlayan bir yasadır. Şirketler, KVKK'ya uygun hareket etmek zorundadır...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/kvkk-foto.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 180,
                        DisplayOrder = 3,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "bilisim-hukuku")?.Id,
                        MetaTitle = "Kişisel Verilerin Korunması Kanunu (KVKK) - Avukat Elif Eylül",
                        MetaDescription = "Kişisel verilerin korunması kanunu (KVKK) hakkında kapsamlı bir inceleme. Şirketlerin uyum sağlaması gereken noktalar.",
                        MetaKeywords = "KVKK, kişisel veriler, veri güvenliği, gizlilik, veri koruma",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "KVKK kapsamında hangi veriler korunuyor?",
                                Answer = "KVKK, kimliği belirli veya belirlenebilir gerçek kişiye ait tüm verileri kapsar. Ad, soyad, TCKN, e-posta, adres, IP adresi, sağlık bilgileri gibi tüm kişisel veriler koruma altındadır."
                            },
                            new BlogFaqItem
                            {
                                Question = "KVKK'ya uyum için ne yapılmalı?",
                                Answer = "KVKK'ya uyum için veri envanteri çıkarılmalı, aydınlatma metinleri hazırlanmalı, veri işleme politikaları oluşturulmalı, çalışan eğitimleri verilmeli ve gerekli güvenlik önlemleri alınmalıdır."
                            },
                            new BlogFaqItem
                            {
                                Question = "KVKK ihlalinin cezası nedir?",
                                Answer = "KVKK ihlallerinde 1.000.000 TL'ye kadar idari para cezası verilebilmektedir. Ayrıca veri ihlali durumunda tazminat davaları da açılabilmektedir."
                            }
                        }
                    },
                    new BlogPost
                    {
                        Title = "Ceza Hukukunda Suç ve Ceza İlişkisi",
                        Slug = "ceza-hukukunda-suc-ve-ceza-iliskisi-1",
                        Summary = "Ceza hukukunda suç ve ceza arasındaki ilişkiyi açıklıyoruz. Suçun tanımından cezaların türlerine kadar geniş bir bakış açısı.",
                        Content = "<p>Ceza hukuku, bir toplumda suçları tanımlayan ve cezalarını belirleyen hukuk dalıdır. Suç, toplumun düzenini bozan her türlü eylem olarak tanımlanabilir. Cezalar ise suçun işlenmesi durumunda devlete başvurulan yaptırımlardır...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/ceza-hukuku.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 200,
                        DisplayOrder = 1,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "ceza-hukuku")?.Id,
                        MetaTitle = "Ceza Hukukunda Suç ve Ceza İlişkisi - Avukat Elif Eylül",
                        MetaDescription = "Ceza hukuku çerçevesinde suç ve ceza arasındaki ilişkiyi ele aldığımız bu yazıyı kaçırmayın.",
                        MetaKeywords = "ceza hukuku, suç, ceza, ceza yasaları",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "Suçun unsurları nelerdir?",
                                Answer = "Suçun unsurları kanuni unsur (suçun yasada tanımlanmış olması), maddi unsur (fiilin gerçekleştirilmesi) ve manevi unsur (kast veya taksir) olarak üçe ayrılır."
                            },
                            new BlogFaqItem
                            {
                                Question = "Ceza türleri nelerdir?",
                                Answer = "Türk ceza hukukunda hapis cezaları (ağırlaştırılmış müebbet, müebbet, süreli hapis) ve güvenlik tedbirleri olmak üzere iki tür ceza bulunmaktadır."
                            },
                            new BlogFaqItem
                            {
                                Question = "Cezalar nasıl belirlenir?",
                                Answer = "Cezalar, suçun niteliği, failin kastı, suçun işleniş şekli, failin önceki sabıkası ve mağdurun durumu gibi faktörler göz önünde bulundurularak belirlenir."
                            }
                        }
                    },
                    new BlogPost
                    {
                        Title = "Ceza Hukukunda İyi Hal İndirimi",
                        Slug = "ceza-hukukunda-iyi-hal-indirimi-2",
                        Summary = "Ceza hukukunda, failin cezasının indirilmesi için hangi durumların geçerli olduğunu inceliyoruz.",
                        Content = "<p>Ceza hukukunda, suç işleyen kişilerin cezalarının, çeşitli sebeplerle indirilmesi mümkündür. Bu sebeplerden biri de iyi hal indirimi olarak bilinir. İyi hal indirimi, suçlunun davranışlarının ve topluma katkılarının göz önünde bulundurulmasıdır...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/iyi-hal-indirimi.jpeg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 180,
                        DisplayOrder = 2,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "ceza-hukuku")?.Id,
                        MetaTitle = "Ceza Hukukunda İyi Hal İndirimi - Avukat Elif Eylül",
                        MetaDescription = "Ceza hukukunda iyi hal indiriminin nasıl uygulandığını ve hangi şartlarla mümkün olduğunu öğrenin.",
                        MetaKeywords = "iyi hal indirimi, ceza hukuku, ceza indirimi",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "İyi hal indirimi nedir?",
                                Answer = "İyi hal indirimi, failin suç öncesi ve sonrası davranışları, toplumdaki saygınlığı, suçtan pişmanlık duyması gibi durumlar göz önünde bulundurularak cezasında yapılan indirimdir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Hangi durumlarda iyi hal indirimi uygulanır?",
                                Answer = "Failin sabıkasız olması, suçtan sonra pişmanlık göstermesi, mağdurla uzlaşması, topluma faydalı işler yapmış olması gibi durumlarda iyi hal indirimi uygulanabilir."
                            },
                            new BlogFaqItem
                            {
                                Question = "İyi hal indirimi ne kadar olabilir?",
                                Answer = "İyi hal indirimi cezanın 1/6'sı oranına kadar uygulanabilir. Ancak bu oran hakimin takdir yetkisine bağlı olarak değişebilir."
                            }
                        }
                    },

                    // Aile Hukuku
                    new BlogPost
                    {
                        Title = "Boşanma Davası ve Hukuki Süreç",
                        Slug = "bosanma-davasi-ve-hukuki-surec-1",
                        Summary = "Boşanma davalarında izlenmesi gereken adımlar ve hukuki sürecin nasıl işlediğine dair kapsamlı bir rehber.",
                        Content = "<p>Boşanma davalarında, tarafların hakları, mal paylaşımı ve çocukların velayeti gibi önemli konular yer almaktadır. Bu yazımızda, boşanma sürecinin nasıl işlediğini ve tarafların nelerle karşılaşacağını anlatacağız...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/bosanma.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 150,
                        DisplayOrder = 1,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "aile-hukuku")?.Id,
                        MetaTitle = "Boşanma Davası ve Hukuki Süreç - Avukat Elif Eylül",
                        MetaDescription = "Boşanma davalarında takip edilmesi gereken hukuki adımlar hakkında bilgi.",
                        MetaKeywords = "boşanma, aile hukuku, boşanma davası, mal paylaşımı, çocuk velayeti",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "Boşanma davası ne kadar sürer?",
                                Answer = "Boşanma davalarının süresi davanın türüne (anlaşmalı veya çekişmeli) ve davanın karmaşıklığına göre değişir. Anlaşmalı boşanmalar genellikle 2-3 ay sürerken, çekişmeli boşanmalar 6 ay ile 2 yıl arasında sürebilir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Boşanmada mal paylaşımı nasıl yapılır?",
                                Answer = "Boşanmada mal paylaşımı, edinilmiş mallara katılma rejimi çerçevesinde yapılır. Evlilik süresince edinilen tüm mallar eşit olarak paylaştırılır. Kişisel mallar ve miras yoluyla elde edilen mallar paylaşıma dahil edilmez."
                            },
                            new BlogFaqItem
                            {
                                Question = "Çocuğun velayeti kime verilir?",
                                Answer = "Velayet, çocuğun menfaati göz önünde bulundurularak mahkeme tarafından belirlenir. Genellikle küçük yaştaki çocukların velayeti anneye, daha büyük çocukların ise kendi tercihleri de dikkate alınarak belirlenir."
                            }
                        }
                    },
                    new BlogPost
                    {
                        Title = "Çocukların Velayeti ve Hukuki Haklar",
                        Slug = "cocuklarin-velayeti-ve-hukuki-haklar-2",
                        Summary = "Boşanma sonrası çocukların velayetinin belirlenmesi ve tarafların hakları üzerine bilgiler.",
                        Content = "<p>Boşanma davalarındaki en önemli konulardan biri, çocukların velayetinin kimde olacağıdır. Bu yazımızda, velayet hakkı ve ilgili hukuki düzenlemeleri ele alacağız...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/velayet.png",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 130,
                        DisplayOrder = 2,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "aile-hukuku")?.Id,
                        MetaTitle = "Çocukların Velayeti ve Hukuki Haklar - Avukat Elif Eylül",
                        MetaDescription = "Çocukların velayetinin belirlenmesi ve boşanma sonrası tarafların hakları hakkında bilgi.",
                        MetaKeywords = "çocuk velayeti, boşanma, aile hukuku, velayet hakları",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "Velayet değişikliği mümkün müdür?",
                                Answer = "Evet, velayet değişikliği mümkündür. Velayetin değiştirilmesi için çocuğun menfaatlerinin velayet kendisinde olan eş tarafından zedelendiğinin ispatlanması gerekir. Önemli ve sürekli değişiklikler olması halinde mahkeme velayeti değiştirebilir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Kişisel ilişki (görüşme) hakkı nedir?",
                                Answer = "Kişisel ilişki hakkı, velayet kendisinde olmayan ebeveynin çocukla düzenli olarak görüşebilmesini sağlayan hukuki bir haktır. Mahkeme tarafından belirlenen gün ve saatlerde çocukla görüşme imkanı tanınır."
                            },
                            new BlogFaqItem
                            {
                                Question = "Velayet için yaş sınırı var mıdır?",
                                Answer = "Kanunda belirli bir yaş sınırı bulunmamakla birlikte, 12 yaşını dolduran çocukların velayet konusundaki tercihleri hakim tarafından dikkate alınır. Ancak nihai karar çocuğun menfaatine göre verilir."
                            }
                        }
                    },

                    // Ticaret Hukuku
                    new BlogPost
                    {
                        Title = "Ticaret Hukukunda Şirket Kuruluşu ve Yasal Süreç",
                        Slug = "ticaret-hukukunda-sirket-kurulusu-ve-yasal-surec-1",
                        Summary = "Ticaret hukuku çerçevesinde şirket kuruluşu sırasında dikkat edilmesi gereken hukuki adımlar.",
                        Content = "<p>Şirket kurarken yasal süreçlerin doğru şekilde takip edilmesi büyük önem taşır. Ticaret hukuku, şirketlerin kuruluş aşamalarını, sözleşmeleri, yönetim yapılarını ve diğer önemli konuları kapsamaktadır...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/sirket-kurulusu.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 200,
                        DisplayOrder = 1,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "ticaret-hukuku")?.Id,
                        MetaTitle = "Ticaret Hukukunda Şirket Kuruluşu ve Yasal Süreç - Avukat Elif Eylül",
                        MetaDescription = "Ticaret hukuku çerçevesinde şirket kurarken dikkat edilmesi gereken önemli hukuki süreçler.",
                        MetaKeywords = "ticaret hukuku, şirket kuruluşu, ticaret yasası, şirket sözleşmesi",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "Limited şirket kurmak için minimum sermaye ne kadar?",
                                Answer = "Limited şirket kurmak için minimum sermaye miktarı 10.000 TL'dir. Bu sermayenin en az %25'i şirket kuruluşunda ödenmek zorundadır."
                            },
                            new BlogFaqItem
                            {
                                Question = "Şirket kuruluşunda hangi belgeler gereklidir?",
                                Answer = "Şirket kuruluşunda ortakların kimlik belgeleri, şirket sözleşmesi, sermaye taahhütnamesi, noter onaylı imza sirküleri ve vergi levhası gibi belgeler gereklidir. Ayrıca Ticaret Sicil Gazetesi'nde ilan yapılması zorunludur."
                            },
                            new BlogFaqItem
                            {
                                Question = "Şirket türleri arasında ne fark var?",
                                Answer = "Limited şirketlerde sorumluluk sermaye ile sınırlıyken, anonim şirketlerde hisse senetleri çıkarılabilir. Şahıs şirketlerinde ise tüzel kişilik yoktur ve işletme sahibinin tüm mal varlığı sorumludur."
                            }
                        }
                    },
                    new BlogPost
                    {
                        Title = "Ticaret Hukukunda Sözleşme Hazırlama ve Uygulama",
                        Slug = "ticaret-hukukunda-sozlesme-hazirlama-ve-uygulama-2",
                        Summary = "Ticaret hukuku kapsamında sözleşmelerin nasıl hazırlanması gerektiği ve yasal geçerliliği üzerine bir rehber.",
                        Content = "<p>Ticaret sözleşmeleri, iş dünyasında en yaygın kullanılan hukuki belgelerdir. Doğru hazırlanmadığı takdirde ciddi hukuki sonuçlar doğurabilir. Bu yazımızda, ticaret sözleşmelerinin nasıl hazırlanması gerektiği üzerinde duracağız...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/sozlesme.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 170,
                        DisplayOrder = 2,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "ticaret-hukuku")?.Id,
                        MetaTitle = "Ticaret Hukukunda Sözleşme Hazırlama ve Uygulama - Avukat Elif Eylül",
                        MetaDescription = "Ticaret hukukunda sözleşme hazırlarken dikkat edilmesi gereken hukuki kurallar ve geçerlilik.",
                        MetaKeywords = "ticaret sözleşmesi, ticaret hukuku, şirket sözleşmeleri, ticaret yasaları",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "Ticari sözleşmelerde hangi unsurlar bulunmalı?",
                                Answer = "Ticari sözleşmelerde tarafların kimlik bilgileri, sözleşme konusu, tarafların hak ve yükümlülükleri, sözleşme süresi, cayma ve fesih şartları, uyuşmazlık çözüm yöntemleri ve cezai şartlar açıkça belirtilmelidir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Sözlü sözleşmeler geçerli midir?",
                                Answer = "Sözlü sözleşmeler genellikle geçerlidir ancak ispatı zordur. Ticari ilişkilerde yazılı sözleşme yapılması, olası uyuşmazlıklarda delil oluşturması açısından büyük önem taşır."
                            },
                            new BlogFaqItem
                            {
                                Question = "Sözleşmede cezai şart nedir?",
                                Answer = "Cezai şart, sözleşmede belirlenen yükümlülüklerin yerine getirilmemesi durumunda ödenecek tazminat miktarını önceden belirleyen hükümdür. Makul olması ve sözleşmede açıkça yer alması gerekir."
                            }
                        }
                    },

                    // Bilişim Hukuku
                    new BlogPost
                    {
                        Title = "Bilişim Hukuku ve Siber Suçlar",
                        Slug = "bilisim-hukuku-ve-siber-suclar-1",
                        Summary = "Bilişim hukuku ve siber suçlar, internet ortamında işlenen suçları ele alır. Bu yazıda siber suçlarla ilgili yasal düzenlemeler hakkında bilgi vereceğiz.",
                        Content = "<p>Bilişim hukuku, dijital ortamda işlenen suçlar, siber saldırılar ve veri güvenliği ile ilgilidir. Türk Ceza Kanunu'nda siber suçlarla ilgili düzenlemeler bulunmaktadır...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/bilisim-suclari.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 150,
                        DisplayOrder = 1,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "bilisim-hukuku")?.Id,
                        MetaTitle = "Bilişim Hukuku ve Siber Suçlar - Avukat Elif Eylül",
                        MetaDescription = "Bilişim hukuku, siber suçlar ve veri güvenliği üzerine bilgi sağlayan kapsamlı bir rehber.",
                        MetaKeywords = "bilişim hukuku, siber suçlar, internet suçları, dijital güvenlik",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "Hangi suçlar siber suç kapsamına girer?",
                                Answer = "Bilişim sistemine girme, verileri yok etme veya değiştirme, banka veya kredi kartlarının kötüye kullanılması, kişisel verileri kaydetme, sistemi engelleme ve siber terör gibi suçlar siber suç kapsamındadır."
                            },
                            new BlogFaqItem
                            {
                                Question = "Siber suçlarda cezalar nelerdir?",
                                Answer = "Siber suçlar Türk Ceza Kanunu'nda düzenlenmiştir. Bu suçlar genellikle 1 yıldan 6 yıla kadar hapis cezası ile cezalandırılır. Ağırlaştırıcı hallerde cezalar artabilir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Siber saldırıya uğradığımı nasıl anlarım?",
                                Answer = "Hesabınızda yetkisiz işlemler, bilgisayarınızda yavaşlama, beklenmedik dosya silinmeleri, şifre değişiklikleri veya finansal işlemlerde anormallikler siber saldırı belirtisi olabilir."
                            }
                        }
                    },
                    new BlogPost
                    {
                        Title = "E-Ticaret ve Hukuki Düzenlemeler",
                        Slug = "e-ticaret-ve-hukuki-duzenlemeler-2",
                        Summary = "E-ticaretin hukuki boyutları ve internet üzerinden ticaretin getirdiği yasal sorumluluklar hakkında bilmeniz gerekenler.",
                        Content = "<p>E-ticaret, son yıllarda hızla büyüyen bir sektör olmuştur. Ancak, e-ticaret yapan şirketlerin ve bireylerin de yasal sorumlulukları bulunmaktadır...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/e-ticaret.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 120,
                        DisplayOrder = 2,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "bilisim-hukuku")?.Id,
                        MetaTitle = "E-Ticaret ve Hukuki Düzenlemeler - Avukat Elif Eylül",
                        MetaDescription = "E-ticaret yapanların ve alışveriş yapanların bilmesi gereken hukuki düzenlemeler hakkında rehber.",
                        MetaKeywords = "e-ticaret, hukuk, internet, yasal düzenlemeler, dijital ticaret",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "E-ticaret sitesi açmak için ne gibi yasal zorunluluklar var?",
                                Answer = "E-ticaret sitesi açmak için Ticaret Sicil Gazetesi'nde ilan, vergi numarası, mesleki sorumluluk sigortası, KVKK uyumluluğu ve uzak mesafeli sözleşmeler yönetmeliğine uygun hareket etmek gerekir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Müşteri cayma hakkını nasıl kullanabilir?",
                                Answer = "Tüketiciler, e-ticaret yoluyla yapılan alışverişlerde 14 gün içinde herhangi bir gerekçe göstermeden cayma hakkını kullanabilir. Ürün orijinal ambalajında ve kullanılmamış olmalıdır."
                            },
                            new BlogFaqItem
                            {
                                Question = "E-ticarette ön ödeme almak yasal mı?",
                                Answer = "Evet, ön ödeme almak yasaldır ancak tüketiciye cayma hakkı bulunduğu bilgisinin verilmesi gerekir. Ön ödeme alındığında tüketiciye fatura kesilmesi zorunludur."
                            }
                        }
                    },
                    new BlogPost
                    {
                        Title = "Miras Hukukunda Mirasçılık ve Paylaşım",
                        Slug = "miras-hukukunda-mirascilik-ve-paylasim-1",
                        Summary = "Miras hukuku çerçevesinde mirasçılık hakları ve mirasın paylaşımı hakkında bilmeniz gerekenler.",
                        Content = "<p>Miras hukuku, ölen kişinin mal varlığının nasıl paylaştırılacağına dair hükümler içerir. Mirasçılar arasında paylaşım yaparken, yasal mirasçılık sırası ve payları göz önünde bulundurulmalıdır...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/miras.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 180,
                        DisplayOrder = 1,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "miras-hukuku")?.Id,
                        MetaTitle = "Miras Hukukunda Mirasçılık ve Paylaşım - Avukat Elif Eylül",
                        MetaDescription = "Miras hukuku hakkında mirasçılık ve mal paylaşımına dair bilmeniz gereken hukuki bilgiler.",
                        MetaKeywords = "miras hukuku, mirasçılık, miras paylaşımı, mal paylaşımı",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "Yasal mirasçılar kimlerdir?",
                                Answer = "Yasal mirasçılar birinci zümre olarak çocuklar ve torunlar, ikinci zümre olarak anne-baba ve kardeşler, üçüncü zümre olarak büyükanne ve büyükbabalardır. Sağ kalan eş her zümreyle birlikte mirasçı olur."
                            },
                            new BlogFaqItem
                            {
                                Question = "Miras paylaşımı nasıl yapılır?",
                                Answer = "Miras paylaşımı, mirasçıların anlaşması halinde miras sözleşmesi ile, anlaşamama durumunda ise mahkeme yoluyla yapılır. Tapu işlemleri için veraset ilamı gereklidir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Miras reddi mümkün müdür?",
                                Answer = "Evet, mirasçılar mirası 3 ay içinde reddedebilir. Mirasın borca batık olması durumunda reddetmek mirasçıyı borçlardan kurtarır. Süresi içinde reddedilmezse miras kabul edilmiş sayılır."
                            }
                        }
                    },
                    new BlogPost
                    {
                        Title = "Vasiyetname Hazırlamanın Hukuki Boyutları",
                        Slug = "vasiyetname-hazirlamanin-hukuki-boyutlari-2",
                        Summary = "Vasiyetname hazırlamanın hukuki önemi ve geçerliliği üzerine rehber.",
                        Content = "<p>Vasiyetname, bir kişinin ölümünden sonra mal varlığının nasıl paylaşılacağına dair yazılı bir talimattır. Bu yazıda, vasiyetnamenin nasıl geçerli olacağı ve dikkat edilmesi gereken unsurlar anlatılmaktadır...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/vasiyet.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 160,
                        DisplayOrder = 2,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "miras-hukuku")?.Id,
                        MetaTitle = "Vasiyetname Hazırlamanın Hukuki Boyutları - Avukat Elif Eylül",
                        MetaDescription = "Vasiyetname hazırlarken dikkat edilmesi gereken hukuki unsurlar hakkında kapsamlı bir rehber.",
                        MetaKeywords = "vasiyetname, miras hukuku, vasiyetname geçerliliği, mal paylaşımı",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "Vasiyetname çeşitleri nelerdir?",
                                Answer = "Resmi vasiyetname (noter veya mahkeme huzurunda), el yazılı vasiyetname (tamamen vasiyet bırakan tarafından el yazısıyla yazılmalı) ve sözlü vasiyetname (olağanüstü hallerde) olmak üzere üç çeşit vasiyetname vardır."
                            },
                            new BlogFaqItem
                            {
                                Question = "Vasiyetname ile mirastan mahrum bırakılabilir mi?",
                                Answer = "Saklı paylı mirasçılar (alt soy, anne-baba, eş) kanundan kaynaklanan paylarından mahrum bırakılamaz. Ancak saklı pay dışında kalan kısım için vasiyetname ile farklı kişilere miras bırakılabilir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Vasiyetname ne zaman açıklanır?",
                                Answer = "Vasiyetname, miras bırakanın ölümünden sonra sulh hukuk mahkemesi tarafından mirasçılara açıklanır. Resmi vasiyetnameler noterde, el yazılı vasiyetnameler ise mahkemeden çıkarılır."
                            }
                        }
                    },

                    // Gayrimenkul Hukuku
                    new BlogPost
                    {
                        Title = "Gayrimenkul Satışı ve Hukuki Yükümlülükler",
                        Slug = "gayrimenkul-satisi-ve-hukuki-yukumlulukler-1",
                        Summary = "Gayrimenkul alım satım işlemleri sırasında dikkat edilmesi gereken hukuki yükümlülükler hakkında bilgi.",
                        Content = "<p>Gayrimenkul alım satımı, büyük bir dikkat ve hukuki prosedür gerektiren bir işlemdir. Satıcı ve alıcı arasındaki sözleşmelerin düzenlenmesi, tapu işlemleri ve vergi yükümlülükleri gibi konular hukuki bir bakış açısıyla ele alınmalıdır...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/gayrimenkul.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 220,
                        DisplayOrder = 1,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "gayrimenkul-hukuku")?.Id,
                        MetaTitle = "Gayrimenkul Satışı ve Hukuki Yükümlülükler - Avukat Elif Eylül",
                        MetaDescription = "Gayrimenkul alım satımı sırasında hukuki yükümlülükler ve dikkat edilmesi gerekenler.",
                        MetaKeywords = "gayrimenkul hukuku, gayrimenkul satışı, tapu işlemleri, alım satım sözleşmesi",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "Tapu devir işlemleri nasıl yapılır?",
                                Answer = "Tapu devir işlemleri için alıcı ve satıcının birlikte tapu dairesine gitmesi, kimlik belgeleri, tapu kaydı, vergi levhası ve tapu harcının ödendiğine dair belge ile başvurması gerekir. Noterden alınan satış vaadi sözleşmesi de gerekebilir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Gayrimenkul satışında hangi vergiler ödenir?",
                                Answer = "Satıcı gelir vergisi, alıcı ise tapu harcı ve emlak vergisi öder. Tapu harcı tapu değeri üzerinden %4 oranında, gelir vergisi ise satış kazancı üzerinden hesaplanır."
                            },
                            new BlogFaqItem
                            {
                                Question = "Kat karşılığı inşaat sözleşmesi nedir?",
                                Answer = "Kat karşılığı inşaat sözleşmesi, arsa sahibi ile müteahhit arasında yapılan, arsanın inşaat maliyeti karşılığında müteahhide belirli sayıda daire verilmesini öngören sözleşmedir. Tapuya şerh verilmesi önemlidir."
                            }
                        }
                    },
                    new BlogPost
                    {
                        Title = "Kiracı Hakları ve Kiraya Verenin Yükümlülükleri",
                        Slug = "kiraci-haklari-ve-kiraya-verenin-yukumlulukleri-2",
                        Summary = "Kiracıların hakları ve kiraya verenin yasal yükümlülükleri hakkında hukuki bir inceleme.",
                        Content = "<p>Kiracıların hakları ve kiraya verenin yükümlülükleri, Türk Borçlar Kanunu'na göre belirlenmiştir. Kiracının tahliyesi, kira bedelinin ödenmesi, sözleşme süresi ve diğer hususlar dikkatle düzenlenmelidir...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/kiraci.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 180,
                        DisplayOrder = 2,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "gayrimenkul-hukuku")?.Id,
                        MetaTitle = "Kiracı Hakları ve Kiraya Verenin Yükümlülükleri - Avukat Elif Eylül",
                        MetaDescription = "Kiracı hakları ve kiraya verenin yükümlülükleri hakkında kapsamlı bilgi.",
                        MetaKeywords = "kiracı hakları, kiraya veren, gayrimenkul hukuku, kira sözleşmesi",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "Kira sözleşmesi en az kaç yıl olmalı?",
                                Answer = "Konut kiraları için asgari sözleşme süresi 1 yıldır. Kiracı bu süre dolmadan tahliye edilemez. 5 yıldan uzun süreli kira sözleşmeleri tapuya şerh verilebilir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Kira artış oranı nasıl belirlenir?",
                                Answer = "Kira artış oranı TÜİK'in açıkladığı enflasyon oranını geçemez. Taraflar anlaşarak daha düşük oranda artış yapabilir. Artış yapılabilmesi için sözleşmede en az 1 yılın dolması gerekir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Kiracı hangi durumlarda tahliye edilebilir?",
                                Answer = "Kiracı, kira bedelini ödememesi, sözleşme koşullarına uymaması, ev sahibinin kendisi veya yakınlarının oturma ihtiyacı olması veya binanın yıkılıp yeniden yapılacak olması durumunda tahliye edilebilir."
                            }
                        }
                    },

                    // İş Hukuku
                    new BlogPost
                    {
                        Title = "İş Hukukunda Çalışan Hakları",
                        Slug = "is-hukukunda-calisan-haklari-1",
                        Summary = "Çalışanların iş hukukunda sahip olduğu haklar, iş güvenliği ve işçi hakları üzerine bilgiler.",
                        Content = "<p>İş hukuku, işçi ve işveren arasındaki ilişkileri düzenler. Çalışanların iş yerindeki hakları, çalışma koşulları, fazla mesai, tatil hakkı gibi konular, işçinin korunmasını amaçlayan yasalarla güvence altına alınmıştır...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/calisan-haklari.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 200,
                        DisplayOrder = 1,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "is-hukuku")?.Id,
                        MetaTitle = "İş Hukukunda Çalışan Hakları - Avukat Elif Eylül",
                        MetaDescription = "İş hukuku kapsamında çalışan hakları ve yasal düzenlemeler hakkında bilmeniz gerekenler.",
                        MetaKeywords = "iş hukuku, çalışan hakları, işçi hakları, çalışma koşulları",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "Haftalık çalışma süresi ne kadardır?",
                                Answer = "4857 sayılı İş Kanunu'na göre haftalık çalışma süresi 45 saattir. Bu sürenin üzerindeki çalışmalar fazla mesai sayılır ve fazla mesai ücreti ödenmelidir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Yıllık izin hakkı ne kadardır?",
                                Answer = "1 yıldan 5 yıla kadar (5 yıl dahil) çalışan işçi 14 gün, 5 yıldan fazla çalışan işçi 20 gün, 18 yaşından küçük işçiler ve maden işçileri ise 30 gün yıllık ücretli izin hakkına sahiptir."
                            },
                            new BlogFaqItem
                            {
                                Question = "İşten çıkarılmada bildirim süresi ne kadardır?",
                                Answer = "6 aya kadar çalışmada 2 hafta, 6 ay-1.5 yıl arası 4 hafta, 1.5-3 yıl arası 6 hafta, 3 yıldan fazla çalışmada 8 hafta bildirim süresi uygulanır. İşveren bu süreyi çalıştırmazsa bildirim tazminatı öder."
                            }
                        }
                    },
                    new BlogPost
                    {
                        Title = "Fazla Mesai ve Ödenmesi Gereken Haklar",
                        Slug = "fazla-mesai-ve-odenmesi-gereken-haklar-2",
                        Summary = "Fazla mesai yapan çalışanların hakları ve ödenmesi gereken ücretler üzerine hukuki bir inceleme.",
                        Content = "<p>Fazla mesai, iş kanunu çerçevesinde çalışanların hakları arasında yer alır. İşverenin fazla mesai ücretini ne zaman ve nasıl ödeyeceği, ilgili yasal düzenlemelerle belirlenmiştir...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/fazla-mesai.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 180,
                        DisplayOrder = 2,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "is-hukuku")?.Id,
                        MetaTitle = "Fazla Mesai ve Ödenmesi Gereken Haklar - Avukat Elif Eylül",
                        MetaDescription = "Fazla mesai yapan çalışanların hakları ve işverenin yükümlülükleri hakkında bilgi.",
                        MetaKeywords = "fazla mesai, iş hukuku, işçi hakları, fazla mesai ücreti",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "Fazla mesai ücreti nasıl hesaplanır?",
                                Answer = "Fazla mesai ücreti, normal saat ücretinin %50 zamlı olarak ödenir. Yani normal çalışma saati ücretinin 1.5 katıdır. Haftalık 45 saati aşan çalışmalar fazla mesai sayılır."
                            },
                            new BlogFaqItem
                            {
                                Question = "Fazla mesai için onay gerekli mi?",
                                Answer = "Evet, fazla mesai yapılabilmesi için işçinin onayı gereklidir. İşveren fazla mesai yapılmasını talep edebilir ancak işçi kabul etmek zorunda değildir. Zorunlu fazla mesai ancak olağanüstü durumlarda yapılabilir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Fazla mesai sınırı var mıdır?",
                                Answer = "Bir günde en fazla 11 saat, haftada en fazla 45 saat (fazla mesai dahil toplam 270 saat/yıl) çalışılabilir. Yıllık fazla mesai sınırı 270 saattir. Bu sınırı aşan fazla mesailer ödenmez."
                            }
                        }
                    },

                    // İcra İflas Hukuku
                    new BlogPost
                    {
                        Title = "İcra İflas Hukukunda Alacaklı Hakları",
                        Slug = "icra-iflas-hukukunda-alacakli-haklari-1",
                        Summary = "İcra iflas hukuku çerçevesinde alacaklıların sahip olduğu haklar ve başvurabileceği yollar hakkında bilgi.",
                        Content = "<p>İcra iflas hukuku, alacaklıların alacaklarını tahsil etmek amacıyla başvurabilecekleri yasal yolları düzenler. Alacaklılar, borçlulara karşı alacaklarının tahsil edilmesi için icra dairelerine başvurabilirler...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/icra.jpg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 210,
                        DisplayOrder = 1,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "icra-iflas-hukuku")?.Id,
                        MetaTitle = "İcra İflas Hukukunda Alacaklı Hakları - Avukat Elif Eylül",
                        MetaDescription = "İcra iflas hukuku çerçevesinde alacaklıların hakları ve yasal haklarını kullanma yolları.",
                        MetaKeywords = "icra hukuku, iflas, alacaklı hakları, icra takibi",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "İcra takibi nasıl başlatılır?",
                                Answer = "İcra takibi, alacaklının icra dairesine başvurması ile başlar. İlamlı (mahkeme kararlı) veya ilamsız (senet, çek, fatura gibi belgelerle) icra takibi yapılabilir. İcra dairesi borçluya ödeme emri gönderir."
                            },
                            new BlogFaqItem
                            {
                                Question = "Ödeme emrine itiraz süresi ne kadardır?",
                                Answer = "Borçlu, ödeme emrinin tebliğinden itibaren 7 gün içinde itiraz edebilir. İtiraz edilmezse borç kesinleşir ve haciz işlemleri başlar. İtiraz edilmesi durumunda icra mahkemesine başvurulur."
                            },
                            new BlogFaqItem
                            {
                                Question = "Hangi mallar haczedilemez?",
                                Answer = "Borçlunun ve ailesinin temel ihtiyaçları (giysi, ev eşyası), mesleki araçlar (belirli değere kadar), sosyal yardımlar, emekli maaşının kanuni kısmı ve kira geliri haczedilemez mallardandır."
                            }
                        }
                    },
                    new BlogPost
                    {
                        Title = "İcra Takibi ve İflas Süreci",
                        Slug = "icra-takibi-ve-iflas-sureci-2",
                        Summary = "İcra takibi ve iflas sürecinin nasıl işlediği, tarafların hakları ve yükümlülükleri hakkında detaylı bir rehber.",
                        Content = "<p>İcra takibi, borçlunun ödeme yükümlülüğünü yerine getirmemesi durumunda başlatılan bir süreçtir. Bu süreç, hukuki yollardan borçlunun mal varlığına el konulmasını içerebilir...</p>",
                        AuthorName = "Av. Elif Eylül",
                        FeaturedImageUrl = "/images/posts/icra-takibi.jpeg",
                        PublishDate = DateTime.UtcNow,
                        IsPublished = true,
                        ViewCount = 190,
                        DisplayOrder = 2,
                        ServiceCategoryId = context.ServiceCategories.FirstOrDefault(x => x.Slug == "icra-iflas-hukuku")?.Id,
                        MetaTitle = "İcra Takibi ve İflas Süreci - Avukat Elif Eylül",
                        MetaDescription = "İcra takibi ve iflas süreci hakkında hukuki bilgi. Alacaklı ve borçluların hakları.",
                        MetaKeywords = "icra takibi, iflas, icra hukuku, borç ödeme, icra davaları",
                        Faqs = new List<BlogFaqItem>
                        {
                            new BlogFaqItem
                            {
                                Question = "İflas nasıl açılır?",
                                Answer = "İflas, alacaklının talebi veya borçlunun kendi başvurusu ile açılabilir. Borçlunun ödeme güçlüğü içinde olması ve borçlarını ödeyememesi durumunda iflas açılır. Ticaret mahkemesi iflas kararı verir."
                            },
                            new BlogFaqItem
                            {
                                Question = "İflasın sonuçları nelerdir?",
                                Answer = "İflas kararı ile borçlunun tüm malları iflas masasına devredilir. Borçlunun tasarruf yetkisi kısıtlanır, malları üzerindeki tasarrufları geçersiz sayılır ve iflas idaresi malları satarak alacaklılara paylaştırır."
                            },
                            new BlogFaqItem
                            {
                                Question = "İcra ve iflas dosyaları ne kadar sürede sonuçlanır?",
                                Answer = "Basit icra takipleri 3-6 ay içinde sonuçlanırken, itiraz edilen dosyalar 1-2 yıl sürebilir. İflas davaları ise genellikle 1-3 yıl arasında sonuçlanır. Süreç dosyanın karmaşıklığına göre değişir."
                            }
                        }
                    }
                };

                await context.BlogPosts.AddRangeAsync(blogPosts);
                await context.SaveChangesAsync();
            }

            if (!await context.Tags.AnyAsync())
            {
                var tags = new List<Tag>
                {
                    new Tag { Name = "Bilişim Hukuku", Slug = "bilisim-hukuku" },
                    new Tag { Name = "Siber Suçlar", Slug = "siber-suclar" },
                    new Tag { Name = "E-Ticaret", Slug = "e-ticaret" },
                    new Tag { Name = "KVKK", Slug = "kvkk" },
                    new Tag { Name = "Veri Güvenliği", Slug = "veri-guvenligi" },
                    new Tag { Name = "Dijital Güvenlik", Slug = "dijital-guvenlik" },
                    new Tag { Name = "Miras Hukuku", Slug = "miras-hukuku" },
                    new Tag { Name = "Vasiyetname", Slug = "vasiyetname" },
                    new Tag { Name = "Gayrimenkul Hukuku", Slug = "gayrimenkul-hukuku" },
                    new Tag { Name = "Kiracı Hakları", Slug = "kiraci-haklari" },
                    new Tag { Name = "İş Hukuku", Slug = "is-hukuku" },
                    new Tag { Name = "Çalışan Hakları", Slug = "calisan-haklari" },
                    new Tag { Name = "Fazla Mesai", Slug = "fazla-mesai" },
                    new Tag { Name = "İcra İflas", Slug = "icra-iflas" },
                    new Tag { Name = "Alacaklı Hakları", Slug = "alacakli-haklari" },
                    new Tag { Name = "İcra Takibi", Slug = "icra-takibi" }
                };

                await context.Tags.AddRangeAsync(tags);
                await context.SaveChangesAsync();
            }

            if (!await context.BlogPostTags.AnyAsync())
            {
                var blogPostTags = new List<BlogPostTag>
                {
                    // "Bilişim Hukuku ve Siber Suçlar" blog yazısına etiketler ekleniyor
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "bilisim-hukuku-ve-siber-suclar-4")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "bilisim-hukuku")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "bilisim-hukuku-ve-siber-suclar-4")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "siber-suclar")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "bilisim-hukuku-ve-siber-suclar-1")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "bilisim-hukuku")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "bilisim-hukuku-ve-siber-suclar-1")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "siber-suclar")?.Id ?? Guid.Empty
                    },
                    // "E-Ticaret ve Hukuki Düzenlemeler" blog yazısına etiketler ekleniyor
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "e-ticaret-ve-hukuki-duzenlemeler-2")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "e-ticaret")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "e-ticaret-ve-hukuki-duzenlemeler-2")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "veri-guvenligi")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "e-ticaret-ve-hukuki-duzenlemeler-4")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "e-ticaret")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "e-ticaret-ve-hukuki-duzenlemeler-4")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "veri-guvenligi")?.Id ?? Guid.Empty
                    },
                    // "Kişisel Verilerin Korunması Kanunu (KVKK)" blog yazısına etiketler ekleniyor
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "kisisel-verilerin-korunmasi-kanunu-kvkk-4")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "kvkk")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "kisisel-verilerin-korunmasi-kanunu-kvkk-4")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "dijital-guvenlik")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "miras-hukukunda-mirascilik-ve-paylasim-1")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "miras-hukuku")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "miras-hukukunda-mirascilik-ve-paylasim-1")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "vasiyetname")?.Id ?? Guid.Empty
                    },
                    // "Vasiyetname Hazırlamanın Hukuki Boyutları" blog yazısına etiketler ekleniyor
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "vasiyetname-hazirlamanin-hukuki-boyutlari-2")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "miras-hukuku")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "vasiyetname-hazirlamanin-hukuki-boyutlari-2")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "vasiyetname")?.Id ?? Guid.Empty
                    },

                    // "Gayrimenkul Satışı ve Hukuki Yükümlülükler" blog yazısına etiketler ekleniyor
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "gayrimenkul-satisi-ve-hukuki-yukumlulukler-1")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "gayrimenkul-hukuku")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "gayrimenkul-satisi-ve-hukuki-yukumlulukler-1")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "kiraci-haklari")?.Id ?? Guid.Empty
                    },
                    // "Kiracı Hakları ve Kiraya Verenin Yükümlülükleri" blog yazısına etiketler ekleniyor
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "kiraci-haklari-ve-kiraya-verenin-yukumlulukleri-2")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "gayrimenkul-hukuku")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "kiraci-haklari-ve-kiraya-verenin-yukumlulukleri-2")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "kiraci-haklari")?.Id ?? Guid.Empty
                    },

                    // "İş Hukukunda Çalışan Hakları" blog yazısına etiketler ekleniyor
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "is-hukukunda-calisan-haklari-1")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "is-hukuku")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "is-hukukunda-calisan-haklari-1")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "calisan-haklari")?.Id ?? Guid.Empty
                    },
                    // "Fazla Mesai ve Ödenmesi Gereken Haklar" blog yazısına etiketler ekleniyor
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "fazla-mesai-ve-odenmesi-gereken-haklar-2")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "is-hukuku")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "fazla-mesai-ve-odenmesi-gereken-haklar-2")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "fazla-mesai")?.Id ?? Guid.Empty
                    },

                    // "İcra İflas Hukukunda Alacaklı Hakları" blog yazısına etiketler ekleniyor
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "icra-iflas-hukukunda-alacakli-haklari-1")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "icra-iflas")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "icra-iflas-hukukunda-alacakli-haklari-1")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "alacakli-haklari")?.Id ?? Guid.Empty
                    },
                    // "İcra Takibi ve İflas Süreci" blog yazısına etiketler ekleniyor
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "icra-takibi-ve-iflas-sureci-2")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "icra-iflas")?.Id ?? Guid.Empty
                    },
                    new BlogPostTag
                    {
                        BlogPostId = context.BlogPosts.FirstOrDefault(x => x.Slug == "icra-takibi-ve-iflas-sureci-2")?.Id ?? Guid.Empty,
                        TagId = context.Tags.FirstOrDefault(x => x.Slug == "icra-takibi")?.Id ?? Guid.Empty
                    }
                };

                await context.BlogPostTags.AddRangeAsync(blogPostTags);
                await context.SaveChangesAsync();
            }


            if (!await context.LawyerProfiles.AnyAsync())
            {
                var serviceCategories = await context.ServiceCategories.ToListAsync();

                // Servisleri isim üzerinden çekelim ki ID'ye gerek kalmasın
                var ceza = serviceCategories.FirstOrDefault(s => s.Slug == "ceza-hukuku");
                var ticaret = serviceCategories.FirstOrDefault(s => s.Slug == "ticaret-hukuku");
                var aile = serviceCategories.FirstOrDefault(s => s.Slug == "aile-hukuku");
                var miras = serviceCategories.FirstOrDefault(s => s.Slug == "miras-hukuku");
                var gayrimenkul = serviceCategories.FirstOrDefault(s => s.Slug == "gayrimenkul-hukuku");
                var isHukuku = serviceCategories.FirstOrDefault(s => s.Slug == "is-hukuku");
                var icra = serviceCategories.FirstOrDefault(s => s.Slug == "icra-iflas-hukuku");
                var bilisim = serviceCategories.FirstOrDefault(s => s.Slug == "bilisim-hukuku");


                // 1. Avukat Profili — Ceza & Ticaret & İş Hukuku
                var lawyer1 = new LawyerProfile
                {
                    FullName = "Av. Elif Eylül",
                    Title = "Kurucu Avukat",
                    ProfileImageUrl = "/images/lawyers/elif-eylul.png",
                    AboutText = "<p>Av. Elif Eylül, Ceza, Ticaret ve İş Hukuku alanlarında uzmanlaşmış olup, 15 yıllık tecrübesiyle müvekkillerine stratejik hukuki çözümler sunmaktadır.</p>",
                    DisplayOrder = 1,
                    IsActive = true
                };

                if (ceza != null)
                    lawyer1.Specializations.Add(new LawyerSpecialization { ServiceCategoryId = ceza.Id, DisplayOrder = 1 });
                if (ticaret != null)
                    lawyer1.Specializations.Add(new LawyerSpecialization { ServiceCategoryId = ticaret.Id, DisplayOrder = 2 });
                if (isHukuku != null)
                    lawyer1.Specializations.Add(new LawyerSpecialization { ServiceCategoryId = isHukuku.Id, DisplayOrder = 3 });
                if (aile != null)
                    lawyer1.Specializations.Add(new LawyerSpecialization { ServiceCategoryId = aile.Id, DisplayOrder = 4 });
                if (miras != null)
                    lawyer1.Specializations.Add(new LawyerSpecialization { ServiceCategoryId = miras.Id, DisplayOrder = 5 });
                if (gayrimenkul != null)
                    lawyer1.Specializations.Add(new LawyerSpecialization { ServiceCategoryId = gayrimenkul.Id, DisplayOrder = 6 });
                if (icra != null)
                    lawyer1.Specializations.Add(new LawyerSpecialization { ServiceCategoryId = icra.Id, DisplayOrder = 7 });
                if (bilisim != null)
                    lawyer1.Specializations.Add(new LawyerSpecialization { ServiceCategoryId = bilisim.Id, DisplayOrder = 7 });

                // Kariyer geçmişi (CareerHistory)
                lawyer1.CareerHistory.Add(new CareerHistory
                {
                    Position = "Kurucu Avukat",
                    Company = "Yılmaz Hukuk Bürosu",
                    StartDate = "2015",
                    EndDate = null,
                    Description = "Ceza ve Ticaret Hukuku alanlarında faaliyet gösteren, geniş müvekkil portföyüne sahip hukuk bürosunun kurucusu.",
                    DisplayOrder = 1
                });

                lawyer1.CareerHistory.Add(new CareerHistory
                {
                    Position = "Kıdemli Avukat",
                    Company = "İstanbul Barosu",
                    StartDate = "2010",
                    EndDate = "2015",
                    Description = "Ceza davaları ve ticari uyuşmazlıklarda aktif olarak görev aldı.",
                    DisplayOrder = 2
                });

                lawyer1.CareerHistory.Add(new CareerHistory
                {
                    Position = "Stajyer Avukat",
                    Company = "Demir & Ortakları Hukuk Bürosu",
                    StartDate = "2008",
                    EndDate = "2010",
                    Description = "Hukuk pratiğine giriş yaptığı dönemde, dava süreçleri ve müvekkil ilişkileri konusunda deneyim kazandı.",
                    DisplayOrder = 3
                });

                await context.LawyerProfiles.AddAsync(lawyer1);


            }



            if (!await context.MenuItems.AnyAsync())
            {
                var menuItems = new List<MenuItem>
                {
                    new MenuItem
                    {
                        Title = "Ana Sayfa",
                        Url = "/",
                        DisplayOrder = 1,
                        IsActive = true,
                        OpenInNewTab = false,
                    },
                    new MenuItem
                    {
                        Title = "Hakkımızda",
                        Url = "/hakkimizda",
                        DisplayOrder = 2,
                        IsActive = true,
                        OpenInNewTab = false,
                    },
                    new MenuItem
                    {
                        Title = "Hizmetlerimiz",
                        Url = "/hizmetler",
                        DisplayOrder = 3,
                        IsActive = true,
                        OpenInNewTab = false,
                    },
                    new MenuItem
                    {
                        Title = "Ekibimiz",
                        Url = "/ekibimiz",
                        DisplayOrder = 4,
                        IsActive = true,
                        OpenInNewTab = false,
                    },
                    new MenuItem
                    {
                        Title = "Blog",
                        Url = "/blog",
                        DisplayOrder = 5,
                        IsActive = true,
                        OpenInNewTab = false,
                    },
                    new MenuItem
                    {
                        Title = "İletişim",
                        Url = "/iletisim",
                        DisplayOrder = 6,
                        IsActive = true,
                        OpenInNewTab = false,
                    }
                };

                await context.MenuItems.AddRangeAsync(menuItems);
            }



            await context.SaveChangesAsync();
        }
    }
}
