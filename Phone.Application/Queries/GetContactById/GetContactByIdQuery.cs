using MediatR;
using Phone.Application.DTOs;
namespace Phone.Queries.GetContactById;
public record GetContactByIdQuery(string Id) : IRequest<ContactDto?>;
