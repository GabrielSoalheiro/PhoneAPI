using AutoMapper;
using MediatR;
using Phone.Domain.Entities;
using Phone.Domain.Interfaces;

namespace Phone.Application.Commands.UpdateContact;

public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, bool>
{
    private readonly IContactRepository _contactRepository;
    private readonly IMapper _mapper;

    public UpdateContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
    {
        _contactRepository = contactRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var existingContact = await _contactRepository.GetByIdAsync(request.Id);
        if (existingContact == null)
            return false;

        var enderecos = _mapper.Map<List<Address>>(request.Enderecos);

        existingContact = new Contact
        {
            Id = request.Id,
            Nome = request.Nome,
            Telefone = request.Telefone,
            Email = request.Email,
            Enderecos = enderecos,
            DataNascimento = request.DataNascimento
        };

        return await _contactRepository.UpdateAsync(request.Id, existingContact);
    }
}