using Interview.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Persistences.EntityTypeConfigurations
{
    internal class JobEntityTypeConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder
                .HasOne(_ => _.Company)
                .WithMany(_ => _.Jobs)
                .HasForeignKey(_ => _.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
            builder
                .HasOne(_ => _.JobCategory)
                .WithMany(_ => _.Jobs)
                .HasForeignKey(_ => _.JobCategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
