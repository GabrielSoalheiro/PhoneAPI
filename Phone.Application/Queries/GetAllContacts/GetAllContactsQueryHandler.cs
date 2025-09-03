using AutoMapper;
using MediatR;
using Phone.Application.DTOs;
using Phone.Domain.Interfaces;

namespace Phone.Application.Queries.GetAllContacts;

public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, IEnumerable<ContactDto>>
{
    private readonly IContactRepository _repo; private readonly IMapper _map;
    public GetAllContactsQueryHandler(IContactRepository r, IMapper m) { _repo = r; _map = m; }
    public async Task<IEnumerable<ContactDto>> Handle(GetAllContactsQuery q, CancellationToken _)
        => _map.Map<IEnumerable<ContactDto>>(await _repo.GetAllAsync());
}
