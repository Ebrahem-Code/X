using MediatR;
using Microsoft.AspNetCore.Mvc;
using X.API.Contracts.Messages;
using X.Application.Messages.Commands.DeleteMessage;
using X.Application.Messages.Commands.SendMessage;
using X.Application.Messages.Commands.UpdateMessage;
using X.Application.Messages.Queries.GetMessageById;
using X.Application.Messages.Queries.GetMessagesByUserId;

namespace X.API.Controllers;


[Route("api/[Controller]")]
[ApiController]
public sealed class MessagesController : ControllerBase
{
    private readonly ISender _sender;

    public MessagesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request, CancellationToken cancellationToken)
    {
        var command = new SendMessageCommand(request.SenderId, request.ReceiverId, request.Content);
        var messageId = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetMessageById), new { id = messageId }, messageId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateMessage(Guid id, [FromBody] UpdateMessageRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateMessageCommand(id, request.NewContent);
        await _sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMessage(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteMessageCommand(id);
        await _sender.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMessageById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetMessageByIdQuery(id);
        var message = await _sender.Send(query, cancellationToken);
        var response = new MessageResponse(message.Id, message.SenderId, message.ReceiverId, message.Content, message.SentAt, message.IsRead);
        return Ok(response);
    }

    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetMessagesByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetMessagesByUserIdQuery(userId);
        var messages = await _sender.Send(query, cancellationToken);
        var response = messages.Select(m => new MessageResponse(m.Id, m.SenderId, m.ReceiverId, m.Content, m.SentAt, m.IsRead)).ToList();
        return Ok(response);
    }
}
