﻿using Interview.Domain.Aggregates.Identities;
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
            builder.HasKey(_ => _.TokenId);
            builder.Property(_ => _.AccessToken).IsRequired();
            builder.Property(_ => _.RefreshToken).IsRequired();
            builder.Property(_ => _.BlagFlag).IsRequired();
        }
    }
}
