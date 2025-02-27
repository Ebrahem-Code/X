using X.Domain.Core.Events;

namespace X.Domain.Messages.Events;

public sealed record MessageUpdatedDomainEvent(Message Message) : DomainEvent;
