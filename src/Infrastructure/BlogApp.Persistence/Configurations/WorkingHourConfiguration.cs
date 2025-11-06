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
    public class WorkingHourConfiguration : IEntityTypeConfiguration<WorkingHour>
    {
        public void Configure(EntityTypeBuilder<WorkingHour> builder)
        {
            builder.ToTable("WorkingHours");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DayNameTr)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.DayNameEn)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Opens)
                .HasMaxLength(10);

            builder.Property(x => x.Closes)
                .HasMaxLength(10);

            builder.Property(x => x.IsClosed)
                .IsRequired();

            // ContactInfo ile ilişki
            builder.HasOne(x => x.ContactInfo)
                .WithMany(x => x.WorkingHours)
                .HasForeignKey(x => x.ContactInfoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
