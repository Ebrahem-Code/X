using X.Application.Core.Data;
using X.Application.Core.Masseges;
using X.Domain.Orders;

namespace X.Application.Orders.Commands.CreateOrder;

public sealed record CreateOrderCommand(
    Guid UserId, 
    string Description, 
    decimal Price) : ICommand<Guid>;

internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = Order.Create(request.UserId, request.Description, request.Price);

        await _orderRepository.AddAsync(order, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
