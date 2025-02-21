using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using X.Domain.Users;

namespace X.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.Id);

        builder.HasIndex(user => user.Id)
            .IsUnique();

        builder.Property(user => user.FirstName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(user => user.LastName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(user => user.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(user => user.Password)
            .HasMaxLength(256)
            .IsRequired();
    }
}
