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
    public class BlogFaqItemConfiguration : IEntityTypeConfiguration<BlogFaqItem>
    {
        public void Configure(EntityTypeBuilder<BlogFaqItem> builder)
        {
            builder.ToTable("BlogFaqItems");

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
