using MediatR;
using Phone.Application.DTOs;
namespace Phone.Application.Queries.GetAllContacts;
public record GetAllContactsQuery : IRequest<IEnumerable<ContactDto>>;
