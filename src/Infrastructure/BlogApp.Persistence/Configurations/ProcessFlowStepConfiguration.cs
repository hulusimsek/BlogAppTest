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
    public class ProcessFlowStepConfiguration : IEntityTypeConfiguration<ProcessFlowStep>
    {
        public void Configure(EntityTypeBuilder<ProcessFlowStep> builder)
        {
            builder.ToTable("ProcessFlowSteps");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.StepNumber)
                .IsRequired();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.IconName)
                .IsRequired()
                .HasMaxLength(100);

            // ServiceDetail ile olan ilişkiyi tanımladık
            builder.HasOne(x => x.ServiceDetail)
                .WithMany(x => x.ProcessFlowSteps)
                .HasForeignKey(x => x.ServiceDetailId)
                .OnDelete(DeleteBehavior.Cascade);  // ServiceDetail silindiğinde ilgili ProcessFlowStep'ler de silinsin.
        }
    }

}
