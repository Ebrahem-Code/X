using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using X.Domain.Notifications;

namespace X.Infrastructure.Configurations;

public sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Id).IsUnique();

        builder.Property(x => x.UserId).IsRequired();

        builder.HasIndex(x => x.UserId).IsUnique();

        builder.Property(x => x.Message).HasMaxLength(500).IsRequired();
    }
}
