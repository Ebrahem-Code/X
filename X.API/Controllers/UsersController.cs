using MediatR;
using Microsoft.AspNetCore.Mvc;
using X.API.Contracts.Users;
using X.Application.Users.Commands.CreateUser;
using X.Application.Users.Commands.DeleteUser;
using X.Application.Users.Commands.UpdateUser;
using X.Application.Users.Queries.GetAllUsers;
using X.Application.Users.Queries.GetUserByEmail;
using X.Application.Users.Queries.GetUserById;

namespace X.API.Controllers;

[Route("api/[Controller]")]
[ApiController]
public sealed class UsersController(ISender sender) : ControllerBase
{
    [HttpPost("Create-User")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request) 
        => Ok(
            await sender.Send(
                new CreateUserCommand(
                    request.FirstName, 
                    request.LastName, 
                    request.Email, 
                    request.Password)));

    [HttpPost("Update-User")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        => Ok(
            await sender.Send(
                new UpdateUserCommand(
                    request.UserId,
                    request.FirstName,
                    request.LastName)));

    [HttpDelete("Delete-User")]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest request)
        => Ok(
            await sender.Send(
                new DeleteUserCommand(request.UserId)));

    [HttpGet("GetAll-Users")]
    public async Task<IActionResult> GetAllUsers()
        => Ok(
            await sender.Send(new GetAllUsersQuery()));

    [HttpGet("Get-User-Id")]
    public async Task<IActionResult> GetUserById([FromQuery] Guid userId)
        => Ok(
            await sender.Send(new GetUserByIdQuery(userId)));

    [HttpGet("Get-User-Email")]
    public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        => Ok(
            await sender.Send(new GetUserByEmailQuery(email)));
}
