namespace X.API.Contracts.Orders;

public sealed record UpdateOrderRequest(
    Guid OrderId,
    string Description,
    decimal Price);
