using X.Domain.Core.Events;

namespace X.Domain.Messages.Events;

public sealed record MessageDeletedDomainEvent(Message Message) : DomainEvent;
