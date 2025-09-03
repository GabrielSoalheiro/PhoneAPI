using AutoMapper;
using MediatR;
using Phone.Application.DTOs;
using Phone.Domain.Entities;
using Phone.Domain.Interfaces;

namespace Phone.Application.Commands.CreateContact;

public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ContactDto>
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public CreateContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    public async Task<ContactDto> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var enderecos = _mapper.Map<List<Address>>(request.Enderecos);

        var contact = new Contact { Nome = request.Nome, Telefone = request.Telefone, Email = request.Email, Enderecos = enderecos, DataNascimento = request.DataNascimento };

        var createdContact = await _contactRepository.CreateAsync(contact);
        return _mapper.Map<ContactDto>(createdContact);
    }
}