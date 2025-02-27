using X.Domain.Core.Events;

namespace X.Domain.Orders.Events;

public sealed record OrderCreatedDomainEvent(Order Order) : DomainEvent;
