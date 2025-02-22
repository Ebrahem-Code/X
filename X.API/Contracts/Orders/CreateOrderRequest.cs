namespace X.API.Contracts.Orders;

public sealed record CreateOrderRequest(
    Guid UserId, 
    string Description, 
    decimal Price);
