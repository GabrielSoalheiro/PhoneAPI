using FluentValidation;
using Phone.Application.Commands.DeleteContact;

namespace Phone.Application.Commands.DeleteContact;

public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
{
    public DeleteContactCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID é obrigatório")
            .Length(24).WithMessage("ID deve ter 24 caracteres");
    }
}