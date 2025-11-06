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
    public class LawyerSpecializationConfiguration : IEntityTypeConfiguration<LawyerSpecialization>
    {
        public void Configure(EntityTypeBuilder<LawyerSpecialization> builder)
        {
            builder.ToTable("LawyerSpecializations");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.LawyerProfile)
                .WithMany(x => x.Specializations)
                .HasForeignKey(x => x.LawyerProfileId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ServiceCategory)
                .WithMany()
                .HasForeignKey(x => x.ServiceCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
