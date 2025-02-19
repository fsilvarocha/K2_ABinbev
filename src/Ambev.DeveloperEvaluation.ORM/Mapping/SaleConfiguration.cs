using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sale");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(s => s.SaleNumber).HasMaxLength(50);
        builder.Property(s => s.SaleDate);
        builder.Property(s => s.Customer).HasMaxLength(50);
        builder.Property(s => s.TotalAmount);
        builder.Property(s => s.Branch).HasMaxLength(50);
        builder.Property(s => s.IsCancelled).HasDefaultValue("true");

        builder.Navigation(s => s.Items)
            .AutoInclude();
    }
}
