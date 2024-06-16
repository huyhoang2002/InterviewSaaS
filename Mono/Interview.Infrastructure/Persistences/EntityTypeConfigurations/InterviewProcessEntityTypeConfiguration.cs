using Interview.Domain.Aggregates.Interviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Persistences.EntityTypeConfigurations
{
    internal sealed class InterviewProcessEntityTypeConfiguration : IEntityTypeConfiguration<InterviewProcess>
    {
        public void Configure(EntityTypeBuilder<InterviewProcess> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.StepKey).IsRequired();
            builder.Property(_ => _.Step).IsRequired();
            builder
                .HasOne(_ => _.InterviewCollection)
                .WithMany(_ => _.Processes)
                .HasForeignKey(_ => _.InterviewCollectionId);
        }
    }
}
