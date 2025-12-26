using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Application;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator) => _mediator = mediator;

        [HttpPost("")]
        public async Task<IActionResult> CreateContact([FromBody] CreateContactCommand command, CancellationToken cancellationToken)
        {
            Guid id = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetByTag), new {tag = command.Tags.First()}, new {id});
        }

        [HttpGet("byTag/{tag}")]
        public async Task<IActionResult> GetByTag([FromRoute] string tag, CancellationToken cancellationToken)
        {
            var contacts = await _mediator.Send(new GetContactsByTagQuery(tag), cancellationToken);
            return Ok(contacts);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateContact(Guid id, [FromBody] UpdateContactCommand contact, CancellationToken cancellationToken)
        {
            if(id != contact.Id)
                return BadRequest("Id is not correct!");

            await _mediator.Send(contact, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteContact(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteContactCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
