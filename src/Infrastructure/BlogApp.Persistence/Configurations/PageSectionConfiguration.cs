using BlogApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Persistence.Configurations
{
    public class PageSectionConfiguration : IEntityTypeConfiguration<PageSection>
    {
        public void Configure(EntityTypeBuilder<PageSection> builder)
        {
            builder.ToTable("PageSection"); // Tablo adını belirler
            builder.HasKey(e => e.Id); // Birincil anahtar

            builder.Property(e => e.Title)
                .IsRequired() // Zorunlu alan
                .HasMaxLength(200); // Maksimum uzunluk

            builder.Property(e => e.Subtitle)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(e => e.Content)
                .HasMaxLength(1000);

            builder.Property(e => e.ButtonText)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ButtonLink)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.BackgroundImageUrl)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.BackgroundAltText)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.MetaTitle)
                .HasMaxLength(200);

            builder.Property(e => e.MetaDescription)
                .HasMaxLength(500);

            builder.Property(e => e.MetaKeywords)
                .HasMaxLength(500);
        }
    }
}
