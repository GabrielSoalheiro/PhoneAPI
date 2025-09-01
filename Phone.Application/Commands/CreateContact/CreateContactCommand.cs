using MediatR;
using Phone.Application.DTOs;

namespace Phone.Application.Commands.CreateContact;

public record CreateContactCommand(
    string Nome,
    string Telefone,
    string Email,
    DateTime? DataNascimento,
    List<CreateAddressDto> Enderecos
) : IRequest<ContactDto>;