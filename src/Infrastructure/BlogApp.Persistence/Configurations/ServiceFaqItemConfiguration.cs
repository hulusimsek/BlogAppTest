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
    public class ServiceFaqItemConfiguration : IEntityTypeConfiguration<ServiceFaqItem>
    {
        public void Configure(EntityTypeBuilder<ServiceFaqItem> builder)
        {
            builder.ToTable("ServiceFaqItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Question)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Answer)
                .IsRequired()
                .HasMaxLength(1000);

            // ServiceDetail ile olan ilişkiyi tanımladık
            builder.HasOne(x => x.ServiceDetail)
                .WithMany(x => x.Faqs)
                .HasForeignKey(x => x.ServiceDetailId)
                .OnDelete(DeleteBehavior.Cascade);  // ServiceDetail silindiğinde ilgili FaqItem'ler de silinsin.
        }
    }
}
