using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItem");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(s => s.ProductName).HasMaxLength(50);
        builder.Property(s => s.Quantity);
        builder.Property(s => s.UnitPrice);
        builder.Property(s => s.Discount);

        builder.HasOne(b => b.Sale)
            .WithMany(c => c.Items)
            .HasForeignKey(b => b.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
