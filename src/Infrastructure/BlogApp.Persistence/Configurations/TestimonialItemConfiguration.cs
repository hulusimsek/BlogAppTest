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
    public class TestimonialItemConfiguration : IEntityTypeConfiguration<TestimonialItem>
    {
        public void Configure(EntityTypeBuilder<TestimonialItem> builder)
        {
            builder.ToTable("TestimonialItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.AuthorName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.AuthorLocation)
                .HasMaxLength(200);  // AuthorLocation opsiyonel olabilir

            // ServiceDetail ile olan ilişkiyi tanımladık
            builder.HasOne(x => x.ServiceDetail)
                .WithMany(x => x.Testimonials)
                .HasForeignKey(x => x.ServiceDetailId)
                .OnDelete(DeleteBehavior.Cascade);  // ServiceDetail silindiğinde ilgili TestimonialItem'lar da silinsin.
        }
    }

}
