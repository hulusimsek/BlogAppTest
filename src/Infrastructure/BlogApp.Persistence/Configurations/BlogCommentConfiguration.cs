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
    public class BlogCommentConfiguration : IEntityTypeConfiguration<BlogComment>
    {
        public void Configure(EntityTypeBuilder<BlogComment> builder)
        {
            builder.ToTable("BlogComments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AuthorName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.AuthorEmail)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.AuthorImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(2000);

            builder.HasOne(x => x.BlogPost)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
