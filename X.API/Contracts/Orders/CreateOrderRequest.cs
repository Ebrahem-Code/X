namespace X.API.Contracts.Orders;

public sealed record CreateOrderRequest
{
    public CreateOrderRequest(Guid userId, string description, decimal price)
    {
        UserId = userId;
        Description = description;
        Price = price;
    }

    public Guid UserId { get; }
    public string Description { get; }
    public decimal Price { get; }
}

