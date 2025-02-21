using MediatR;
using Microsoft.AspNetCore.Mvc;
using X.API.Contracts.Users;
using X.Application.Users.Commands.CreateUser;

namespace X.API.Controllers;

[Route("api/[Controller]")]
[ApiController]
public sealed class UsersController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var command = new CreateUserCommand(request.FirstName, request.LastName, request.Email, request.Password);
        await sender.Send(command);
        return Ok();
    }
}
