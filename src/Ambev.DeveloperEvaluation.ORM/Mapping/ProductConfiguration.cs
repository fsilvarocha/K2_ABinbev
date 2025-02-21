using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);


        builder
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
