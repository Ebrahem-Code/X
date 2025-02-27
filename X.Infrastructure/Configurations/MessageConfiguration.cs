using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using X.Domain.Messages;

namespace X.Infrastructure.Configurations;

internal sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SenderId).IsRequired();

        builder.Property(x => x.ReceiverId).IsRequired();

        builder.Property(x => x.Content).IsRequired().HasMaxLength(1000);

        builder.Property(x => x.SentAt).IsRequired();

        builder.Property(x => x.IsRead).IsRequired();

        builder.HasIndex(x => new { x.SenderId, x.ReceiverId });
    }
}
