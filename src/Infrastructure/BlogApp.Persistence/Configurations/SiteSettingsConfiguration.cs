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
    public class SiteSettingsConfiguration : IEntityTypeConfiguration<SiteSettings>
    {
        public void Configure(EntityTypeBuilder<SiteSettings> builder)
        {
            builder.ToTable("SiteSettings");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.SiteName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.LogoUrl)
                .HasMaxLength(500);

            builder.Property(e => e.ButtonText)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.ButtonLink)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(e => e.ButtonAltText)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.FooterText)
                .HasMaxLength(1000);

            builder.Property(x => x.CopyrightText)
                .HasMaxLength(500);

            builder.Property(x => x.TwitterUrl)
                .HasMaxLength(500);

            builder.Property(x => x.LinkedInUrl)
                .HasMaxLength(500);

            builder.Property(x => x.FacebookUrl)
                .HasMaxLength(500);

            builder.Property(x => x.InstagramUrl)
                .HasMaxLength(500);

            builder.Property(x => x.IsActive)
                    .HasDefaultValue(true);

            builder.Property(x => x.ButtonIsActive)
                .HasDefaultValue(true);

            // 🎯 Favicons - Owned Type (Alt nesne olarak tabloya gömülür)
            builder.OwnsOne(x => x.Favicons, favicon =>
            {
                favicon.Property(f => f.Favicon16)
                    .HasColumnName("Favicon16")
                    .HasMaxLength(300);

                favicon.Property(f => f.Favicon32)
                    .HasColumnName("Favicon32")
                    .HasMaxLength(300);

                favicon.Property(f => f.Favicon180)
                    .HasColumnName("Favicon180")
                    .HasMaxLength(300);

                favicon.Property(f => f.Favicon192)
                    .HasColumnName("Favicon192")
                    .HasMaxLength(300);

                favicon.Property(f => f.Favicon512)
                    .HasColumnName("Favicon512")
                    .HasMaxLength(300);
            });
        }
    }

}
