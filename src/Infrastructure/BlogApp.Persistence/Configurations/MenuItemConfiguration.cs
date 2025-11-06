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
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("MenuItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Url)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne(x => x.ParentMenuItem)
                .WithMany(x => x.SubMenuItems)
                .HasForeignKey(x => x.ParentMenuItemId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
