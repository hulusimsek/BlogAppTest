using BlogApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Persistence.Configurations
{
    public class ContactFormSettingsConfiguration : IEntityTypeConfiguration<ContactFormSettings>
    {
        public void Configure(EntityTypeBuilder<ContactFormSettings> builder)
        {
            // Tablo adı
            builder.ToTable("ContactFormSettings");

            // Primary key
            builder.HasKey(x => x.Id);

            // Column configurations for each property
            builder.Property(x => x.SectionKey)
                .HasMaxLength(100); // Genel olarak "SectionKey" kısadır, max 100 karakter yeterli olabilir

            builder.Property(x => x.SmallBadgeText)
                .HasMaxLength(100); // Badge metni de genelde kısa olur

            builder.Property(x => x.Title)
                .HasMaxLength(200);

            builder.Property(x => x.SubtitleTemplate)
                .HasMaxLength(500);

            builder.Property(x => x.LabelServiceCategory)
                .HasMaxLength(150);

            builder.Property(x => x.LabelServiceCategoryDescription)
                .HasMaxLength(200);

            builder.Property(x => x.LabelFullName)
                .HasMaxLength(150);

            builder.Property(x => x.PlaceholderFullName)
                .HasMaxLength(200);

            builder.Property(x => x.LabelEmail)
                .HasMaxLength(100);

            builder.Property(x => x.PlaceholderEmail)
                .HasMaxLength(200);

            builder.Property(x => x.LabelPhone)
                .HasMaxLength(100);

            builder.Property(x => x.PlaceholderPhoneDisplay)
                .HasMaxLength(100);

            builder.Property(x => x.PhonePrefixDisplay)
                .HasMaxLength(10);

            builder.Property(x => x.LabelSubject)
                .HasMaxLength(150);

            builder.Property(x => x.PlaceholderSubject)
                .HasMaxLength(200);

            builder.Property(x => x.LabelMessage)
                .HasMaxLength(150);

            builder.Property(x => x.PlaceholderMessage)
                .HasMaxLength(2000); // Mesaj daha uzun olabileceği için büyük bir uzunluk

            builder.Property(x => x.KvkkTextTemplate)
                .HasMaxLength(500); // KVKK metni genelde kısa ama öz olur, 500 karakter yeterlidir

            builder.Property(x => x.KvkkLink)
                .HasMaxLength(200); // Linkler genellikle kısa olur, 200 karakter yeterli

            builder.Property(x => x.SubmitButtonText)
                .HasMaxLength(100);

            builder.Property(x => x.IsActive)
                .IsRequired(); // Bu özellik her zaman belirli bir değere sahip olacak

            builder.Property(x => x.IsActive)
                .HasDefaultValue(true);
        }
    }

}