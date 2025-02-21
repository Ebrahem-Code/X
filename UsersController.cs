using MediatR;
using Microsoft.AspNetCore.Mvc;
using X.API.Contracts.Users;
using X.Application.Users.Commands.CreateUser;
using FluentValidation;

namespace X.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class UsersController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IValidator<CreateUserRequest> _validator;

    public UsersController(ISender sender, IValidator<CreateUserRequest> validator)
    {
        _sender = sender;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var command = new CreateUserCommand(request.FirstName, request.LastName, request.Email, request.Password);
        try
        {
            await _sender.Send(command);
            return CreatedAtAction(nameof(CreateUser), new { email = request.Email }, request);
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here as needed
            return StatusCode(500, "An error occurred while creating the user.");
        }
    }
}
