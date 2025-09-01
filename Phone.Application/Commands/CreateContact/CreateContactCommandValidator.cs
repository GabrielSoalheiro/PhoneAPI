using FluentValidation;
using Phone.Domain.Interfaces;

namespace Phone.Application.Commands.CreateContact;

public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
{
    private readonly IContactRepository _contactRepository;

    public CreateContactCommandValidator(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .Length(2, 100).WithMessage("Nome deve ter entre 2 e 100 caracteres")
            .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage("Nome deve conter apenas letras e espaços");

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("Telefone é obrigatório")
            .Matches(@"^\(\d{2}\)\s\d{4,5}-\d{4}$")
            .WithMessage("Telefone deve estar no formato (99) 99999-9999 ou (99) 9999-9999")
            .MustAsync(async (telefone, cancellation) => !await _contactRepository.ExistsByTelefoneAsync(telefone))
            .WithMessage("Este telefone já está cadastrado");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório")
            .EmailAddress().WithMessage("Email deve ter um formato válido")
            .MustAsync(async (email, cancellation) => !await _contactRepository.ExistsByEmailAsync(email))
            .WithMessage("Este email já está cadastrado");

        RuleFor(x => x.DataNascimento)
            .LessThan(DateTime.Now).WithMessage("Data de nascimento deve ser anterior à data atual")
            .GreaterThan(DateTime.Now.AddYears(-120)).WithMessage("Data de nascimento não pode ser superior a 120 anos")
            .When(x => x.DataNascimento.HasValue);

        RuleFor(x => x.Enderecos)
            .NotEmpty().WithMessage("Pelo menos um endereço é obrigatório")
            .Must(x => x.Count <= 5).WithMessage("Máximo de 5 endereços permitidos");

        RuleForEach(x => x.Enderecos).ChildRules(endereco =>
        {
            endereco.RuleFor(e => e.Logradouro)
                .NotEmpty().WithMessage("Logradouro é obrigatório")
                .Length(5, 100).WithMessage("Logradouro deve ter entre 5 e 100 caracteres");

            endereco.RuleFor(e => e.Numero)
                .NotEmpty().WithMessage("Número é obrigatório")
                .Length(1, 10).WithMessage("Número deve ter entre 1 e 10 caracteres");

            endereco.RuleFor(e => e.Bairro)
                .NotEmpty().WithMessage("Bairro é obrigatório")
                .Length(2, 50).WithMessage("Bairro deve ter entre 2 e 50 caracteres");

            endereco.RuleFor(e => e.Cidade)
                .NotEmpty().WithMessage("Cidade é obrigatória")
                .Length(2, 50).WithMessage("Cidade deve ter entre 2 e 50 caracteres");

            endereco.RuleFor(e => e.Estado)
                .NotEmpty().WithMessage("Estado é obrigatório")
                .Length(2, 2).WithMessage("Estado deve ter exatamente 2 caracteres")
                .Matches(@"^[A-Z]{2}$").WithMessage("Estado deve conter apenas letras maiúsculas");

            endereco.RuleFor(e => e.CEP)
                .NotEmpty().WithMessage("CEP é obrigatório")
                .Matches(@"^\d{5}-?\d{3}$").WithMessage("CEP deve estar no formato 99999-999 ou 99999999");
        });
    }
}