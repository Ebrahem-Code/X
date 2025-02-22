using X.Domain.Core.BaseEntity;

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

    public Guid UserId { get; private set; }
    public string Description { get; private set; } = default!;
    public decimal Price { get; private set; }

    public static Order Create(Guid userId, string description, decimal price)
    {
        return new Order(userId, description, price);
    }

    public void Update(string description, decimal price)
    {
        Description = description;
        Price = price;
    }
}
