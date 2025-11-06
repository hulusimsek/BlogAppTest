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
    public class LawyerProfileConfiguration : IEntityTypeConfiguration<LawyerProfile>
    {
        public void Configure(EntityTypeBuilder<LawyerProfile> builder)
        {
            builder.ToTable("LawyerProfiles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.ProfileImageUrl)
                .HasMaxLength(500);

            builder.Property(x => x.AboutText)
                .HasColumnType("LONGTEXT");

            builder.HasMany(x => x.Specializations)
                .WithOne(x => x.LawyerProfile)
                .HasForeignKey(x => x.LawyerProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.CareerHistory)
                .WithOne(x => x.LawyerProfile)
                .HasForeignKey(x => x.LawyerProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
