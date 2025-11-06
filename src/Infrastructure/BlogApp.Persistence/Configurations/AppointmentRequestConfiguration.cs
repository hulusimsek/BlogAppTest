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
    public class AppointmentRequestConfiguration : IEntityTypeConfiguration<AppointmentRequest>
    {
        public void Configure(EntityTypeBuilder<AppointmentRequest> builder)
        {
            builder.ToTable("AppointmentRequests");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Phone)
                .HasMaxLength(50);

            builder.Property(x => x.Message)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(x => x.Notes)
                .HasMaxLength(1000);

            builder.HasOne(x => x.ServiceCategory)
                .WithMany()
                .HasForeignKey(x => x.ServiceCategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
