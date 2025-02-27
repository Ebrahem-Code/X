using X.Domain.Core.BaseEntity;
using X.Domain.Orders.Events;

namespace X.Domain.Orders;

public sealed class Order : AggregateRoot
{
    private Order(Guid userId, string description, decimal price)
        : base(Guid.NewGuid())
    {
        UserId = userId;
        Description = description;
        Price = price;
    }
    private Order() { }

    public Guid UserId { get; } 
    public string Description { get; private set; } = default!;
    public decimal Price { get; private set; }

    public static Order Create(Guid userId, string description, decimal price)
    {
        Order order = new Order(userId, description, price);

        // Raise DomainEvent.
        order.AddDomainEvent(new OrderCreatedDomainEvent(order));

        return order;
    }

    public void Update(string description, decimal price)
    {
        Description = description;
        Price = price;

        // Raise DomainEvent.
        this.AddDomainEvent(new OrderUpdatedDomainEvent(this));
    }

    public void Delete()
    {
        // Raise DomainEvent.
        this.AddDomainEvent(new OrderDeletedDomainEvent(this));
    }
}
