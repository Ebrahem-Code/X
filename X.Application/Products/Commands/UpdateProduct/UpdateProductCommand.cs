﻿using X.Application.Core.CQRS;

namespace X.Application.Products.Commands.UpdateProduct;

public sealed record UpdateProductCommand(
    Guid ProductId, 
    string Name,
    string Description,
    decimal Price,
    int Stock) : ICommand<Guid>;
