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
    public class ContactInfoFaqItemConfiguration : IEntityTypeConfiguration<ContactInfoFaqItem>
    {
        public void Configure(EntityTypeBuilder<ContactInfoFaqItem> builder)
        {
            builder.ToTable("ContactInfoFaqItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Question)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Answer)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}
