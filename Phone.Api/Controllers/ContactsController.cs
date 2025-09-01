using MediatR;
using Microsoft.AspNetCore.Mvc;
using Phone.Application.Commands.CreateContact;
using Phone.Application.Commands.DeleteContact;
using Phone.Application.Commands.UpdateContact;
using Phone.Application.DTOs;
using Phone.Application.Queries.GetAllContacts;
using Phone.Queries.GetContactById;

namespace Phone.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IMediator _mediator;
    public ContactsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IEnumerable<ContactDto>> GetAll()
        => await _mediator.Send(new GetAllContactsQuery());

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetById(string id)
        => Ok(await _mediator.Send(new GetContactByIdQuery(id)));

    [HttpPost]
    public async Task<IActionResult> Create(CreateContactCommand cmd)
    {
        var res = await _mediator.Send(cmd);
        return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, UpdateContactCommand cmd)
        => await _mediator.Send(cmd with { Id = id }) ? NoContent() : NotFound();

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
        => await _mediator.Send(new DeleteContactCommand(id)) ? NoContent() : NotFound();
}
