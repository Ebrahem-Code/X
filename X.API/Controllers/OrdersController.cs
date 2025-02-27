using MediatR;
using Microsoft.AspNetCore.Mvc;
using X.API.Contracts.Orders;

namespace X.API.Controllers;

[Route("api/[Controller]")]
[ApiController]
public sealed class OrdersController(ISender sender) : ControllerBase
{
    [HttpPost("Create-Order")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request) => Ok(await sender.Send());

    [HttpPut("Update-Order")]
    public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderRequest request) => Ok(await sender.Send());


    [HttpDelete("Delete-Order")]
    public async Task<IActionResult> DeleteOrder([FromBody] DeleteOrderRequest request) => Ok(await sender.Send());


    [HttpGet("Get-All-orders")]
    public async Task<IActionResult> GetAllUsersOrder() => Ok(await sender.Send());

    [HttpGet("Get-By-Id")]
    public async Task<IActionResult> GetOrderById([FromQuery] Guid userId) => Ok(await sender.Send());
}
