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
    public class CareerHistoryConfiguration : IEntityTypeConfiguration<CareerHistory>
    {
        public void Configure(EntityTypeBuilder<CareerHistory> builder)
        {
            builder.ToTable("CareerHistories");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Position)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Company)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.StartDate)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.EndDate)
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasMaxLength(1000);

            builder.HasOne(x => x.LawyerProfile)
                .WithMany(x => x.CareerHistory)
                .HasForeignKey(x => x.LawyerProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
