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
    internal sealed class InterviewCollectionEntityTypeConfiguration : IEntityTypeConfiguration<InterviewCollection>
    {
        public void Configure(EntityTypeBuilder<InterviewCollection> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.CollectionName).IsRequired();
            builder.HasIndex(_ => _.CollectionName).IsUnique();
        }
    }
}
