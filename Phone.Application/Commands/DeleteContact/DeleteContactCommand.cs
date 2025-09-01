using MediatR;

namespace Phone.Application.Commands.DeleteContact;

public record DeleteContactCommand(string Id) : IRequest<bool>;