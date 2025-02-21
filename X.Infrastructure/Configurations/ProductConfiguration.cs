using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using X.Domain.Products;

namespace X.Infrastructure.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id);

        builder.HasIndex(product => product.Id).IsUnique();

        builder.Property(product => product.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(product => product.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(product => product.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
    }
}
