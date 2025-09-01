using MediatR;
using Phone.Application.DTOs;

namespace Phone.Application.Commands.UpdateContact;

public record UpdateContactCommand(
    string Id,
    string Nome,
    string Telefone,
    string Email,
    DateTime? DataNascimento,
    List<CreateAddressDto> Enderecos
) : IRequest<bool>;