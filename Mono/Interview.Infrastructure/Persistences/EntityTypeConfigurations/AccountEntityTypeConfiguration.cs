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
    internal class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(_ => _.Id);

            var tokenNavigation = builder.Metadata.FindNavigation(nameof(Account.Tokens));
            tokenNavigation.SetPropertyAccessMode(PropertyAccessMode.Property);
        }
    }
}
