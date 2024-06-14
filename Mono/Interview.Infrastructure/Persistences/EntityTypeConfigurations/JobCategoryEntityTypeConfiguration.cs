using Interview.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Persistences.EntityTypeConfigurations
{
    internal class JobCategoryEntityTypeConfiguration : IEntityTypeConfiguration<JobCategory>
    {
        public void Configure(EntityTypeBuilder<JobCategory> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.CategoryName).IsRequired();
            builder.HasIndex(_ => _.CategoryName).IsUnique();
            builder
                .HasOne(_ => _.Company)
                .WithMany(_ => _.JobCategories)
                .HasForeignKey(_ => _.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
