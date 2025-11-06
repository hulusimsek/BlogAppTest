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
    public class ServiceDetailConfiguration : IEntityTypeConfiguration<ServiceDetail>
    {
        public void Configure(EntityTypeBuilder<ServiceDetail> builder)
        {
            builder.ToTable("ServiceDetails");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.DetailedDescription)
                .HasColumnType("LONGTEXT");

            builder.HasOne(x => x.ServiceCategory)
                .WithOne(x => x.ServiceDetail)
                .HasForeignKey<ServiceDetail>(x => x.ServiceCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // ServiceCategory ile olan ilişkiyi tanımladık
            builder.HasOne(x => x.ServiceCategory)
                .WithOne(x => x.ServiceDetail)
                .HasForeignKey<ServiceDetail>(x => x.ServiceCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // ProcessFlowSteps ile ilişkili olma
            builder.HasMany(x => x.ProcessFlowSteps)
                .WithOne(x => x.ServiceDetail)
                .HasForeignKey(x => x.ServiceDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            // FaqItems ile ilişkili olma
            builder.HasMany(x => x.Faqs)
                .WithOne(x => x.ServiceDetail)
                .HasForeignKey(x => x.ServiceDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            // TestimonialItems ile ilişkili olma
            builder.HasMany(x => x.Testimonials)
                .WithOne(x => x.ServiceDetail)
                .HasForeignKey(x => x.ServiceDetailId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
