using ApiPractice.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ApiPractice.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            // builder.Property(x => x.CreatedTime).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.CreatedTime).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
