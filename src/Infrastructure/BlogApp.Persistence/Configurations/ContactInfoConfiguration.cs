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
    public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.ToTable("ContactInfos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.MapEmbedUrl)
                .HasMaxLength(1000);

            // FaqItems ile ilişkili olma
            builder.HasMany(x => x.Faqs)
                .WithOne(x => x.ContactInfo)
                .HasForeignKey(x => x.ContactInfoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
