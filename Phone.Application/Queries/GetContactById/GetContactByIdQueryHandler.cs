using AutoMapper;
using MediatR;
using Phone.Application.DTOs;
using Phone.Domain.Interfaces;

namespace Phone.Queries.GetContactById;

public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ContactDto?>
{
    private readonly IContactRepository _repo; private readonly IMapper _map;
    public GetContactByIdQueryHandler(IContactRepository r, IMapper m) { _repo = r; _map = m; }
    public async Task<ContactDto?> Handle(GetContactByIdQuery q, CancellationToken _)
        => _map.Map<ContactDto>(await _repo.GetByIdAsync(q.Id));
}
