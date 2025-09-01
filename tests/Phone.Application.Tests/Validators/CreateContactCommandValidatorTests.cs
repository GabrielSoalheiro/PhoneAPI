using FluentValidation.TestHelper;
using Moq;
using Phone.Application.Commands.CreateContact;
using Phone.Domain.Interfaces;
using Phone.Application.DTOs;
using Xunit;
using System.Collections.Generic;

namespace Phone.Application.Tests.Validators;

public class CreateContactCommandValidatorTests
{
    private readonly CreateContactCommandValidator _validator;

    public CreateContactCommandValidatorTests()
    {
        var repo = new Mock<IContactRepository>();
        repo.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>(), null)).ReturnsAsync(false);
        repo.Setup(r => r.ExistsByTelefoneAsync(It.IsAny<string>(), null)).ReturnsAsync(false);
        _validator = new CreateContactCommandValidator(repo.Object);
    }

    [Fact]
    public void Nome_Empty_Should_Have_Error()
    {
        var cmd = new CreateContactCommand("", "(11) 99999-9999", "a@a.com", null,
            new List<CreateAddressDto>
            {
                new CreateAddressDto
                {
                    Logradouro = "Rua",
                    Numero = "1",
                    Complemento = null,
                    Bairro = "Bairro",
                    Cidade = "Cidade",
                    Estado = "SP",
                    CEP = "01000-000"
                }
            });

        var result = _validator.TestValidate(cmd);
        result.ShouldHaveValidationErrorFor(c => c.Nome);
    }

    [Fact]
    public void Telefone_Invalid_Should_Have_Error()
    {
        var cmd = new CreateContactCommand("Ok", "11999999999", "a@a.com", null,
            new List<CreateAddressDto>
            {
                new CreateAddressDto
                {
                    Logradouro = "Rua",
                    Numero = "1",
                    Complemento = null,
                    Bairro = "Bairro",
                    Cidade = "Cidade",
                    Estado = "SP",
                    CEP = "01000-000"
                }
            });

        _validator.TestValidate(cmd)
                  .ShouldHaveValidationErrorFor(c => c.Telefone);
    }
}
