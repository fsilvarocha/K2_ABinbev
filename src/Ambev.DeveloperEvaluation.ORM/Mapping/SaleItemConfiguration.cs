using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        builder.HasKey(si => new { si.SaleId, si.ProductId });

        builder.HasOne(si => si.Sale)
            .WithMany(s => s.SaleItems)
            .HasForeignKey(si => si.SaleId);

        builder.HasOne(si => si.Product)
            .WithMany(p => p.SaleItems)
            .HasForeignKey(si => si.ProductId);

    }
}
