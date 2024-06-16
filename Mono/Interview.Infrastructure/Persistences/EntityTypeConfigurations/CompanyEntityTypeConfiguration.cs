using Interview.Domain.Aggregates.Companies;
using Interview.Domain.Aggregates.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.Persistences.EntityTypeConfigurations
{
    internal class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property<Guid>(_ => _.Id).ValueGeneratedOnAdd();

            var AddressNavigation = builder.Metadata.FindNavigation(nameof(Company.CompanyAddresses));
            AddressNavigation.SetPropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
