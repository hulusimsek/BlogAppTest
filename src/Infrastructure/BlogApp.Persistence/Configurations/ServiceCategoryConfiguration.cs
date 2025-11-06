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
    public class ServiceCategoryConfiguration : IEntityTypeConfiguration<ServiceCategory>
    {
        public void Configure(EntityTypeBuilder<ServiceCategory> builder)
        {
            builder.ToTable("ServiceCategories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(x => x.Slug)
                .IsUnique();

            builder.Property(x => x.IconName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ShortDescription)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.MetaTitle)
                .HasMaxLength(200);

            builder.Property(x => x.MetaDescription)
                .HasMaxLength(500);

            builder.Property(x => x.MetaKeywords)
                .HasMaxLength(500);

            builder.HasMany(x => x.BlogPosts)
                .WithOne(x => x.ServiceCategory)
                .HasForeignKey(x => x.ServiceCategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(x => x.ServiceDetail)
                .WithOne(x => x.ServiceCategory)
                .HasForeignKey<ServiceDetail>(x => x.ServiceCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Bu kısmı değiştireceğiz:
            builder.HasMany(x => x.LawyerSpecializations)
                .WithOne(x => x.ServiceCategory)
                .HasForeignKey(x => x.ServiceCategoryId)
                .OnDelete(DeleteBehavior.Cascade);  // OnDelete'ı Cascade olarak değiştiriyoruz
        }
    }

}
