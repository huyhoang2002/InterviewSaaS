using Interview.Domain.Aggregates.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Persistences.EntityTypeConfigurations
{
    internal class RatingEntityTypeConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property<Guid>(_ => _.Id).ValueGeneratedOnAdd();
            builder
                .HasOne(_ => _.Company)
                .WithMany(_ => _.Ratings)
                .HasForeignKey(_ => _.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
