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
    internal class TokenEntityTypeConfiguration : IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id).ValueGeneratedOnAdd();
            builder.Property(_ => _.AccessToken).IsRequired();
            builder.Property(_ => _.RefreshToken).IsRequired();
            builder.Property(_ => _.BlagFlag).IsRequired();
            builder.HasOne(_ => _.Account).WithMany(_ => _.Tokens).HasForeignKey(_ => _.AccountId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
