using X.Domain.Core.BaseEntity;

namespace X.Domain.Products;

public sealed class Product : AggregateRoot
{
    private Product(string name, string description, decimal price, int stock)
        : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }

    private Product() { }

    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;
    public int Stock { get; private set; } = default!;

    public static Product Create(string name, string description, decimal price, int stock)
    {
        return new Product(name, description, price, stock);
    }

    public void UpdateProduct(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    public void UpdateStock(int stock)
    {
        Stock = stock;
    }
}
