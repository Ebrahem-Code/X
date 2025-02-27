using X.Domain.Core.Events;

namespace X.Domain.Orders.Events;

public sealed record OrderUpdatedDomainEvent(Order Order) : DomainEvent;
