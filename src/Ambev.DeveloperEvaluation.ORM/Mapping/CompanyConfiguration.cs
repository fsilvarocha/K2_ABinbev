using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companys");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);

        builder.HasOne(hf => hf.HeadOffice)
            .WithMany(b => b.Branches)
            .HasForeignKey(b => b.HeadOfficeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
