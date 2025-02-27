using X.Domain.Core.Events;

namespace X.Domain.Orders.Events;

public sealed record OrderDeletedDomainEvent(Order Order) : DomainEvent;
