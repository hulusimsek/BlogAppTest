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
    public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.ToTable("BlogPosts");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(x => x.Slug)
                .IsRequired()
                .HasMaxLength(300);

            builder.HasIndex(x => x.Slug)
                .IsUnique();

            builder.Property(x => x.Summary)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasColumnType("LONGTEXT");

            builder.Property(x => x.FeaturedImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.AuthorName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.AuthorImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.MetaTitle)
                .HasMaxLength(200);

            builder.Property(x => x.MetaDescription)
                .HasMaxLength(500);

            builder.Property(x => x.MetaKeywords)
                .HasMaxLength(500);

            // ServiceCategoryId silinmesin, sadece NULL yapılmasını istiyoruz.
            builder.HasOne(x => x.ServiceCategory)
                .WithMany(x => x.BlogPosts)
                .HasForeignKey(x => x.ServiceCategoryId)
                .OnDelete(DeleteBehavior.SetNull);  // NULL yapılacak

            // FaqItems ile ilişkili olma
            builder.HasMany(x => x.Faqs)
                .WithOne(x => x.Blog)
                .HasForeignKey(x => x.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.BlogPost)
                .HasForeignKey(x => x.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Tags)
                .WithOne(x => x.BlogPost)
                .HasForeignKey(x => x.BlogPostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
