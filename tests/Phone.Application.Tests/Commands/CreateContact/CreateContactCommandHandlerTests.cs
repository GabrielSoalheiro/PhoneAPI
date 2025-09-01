using AutoMapper;
using FluentAssertions;
using Moq;
using Phone.Application.Commands.CreateContact;
using Phone.Application.DTOs;
using Phone.Application.Mappings;
using Phone.Domain.Entities;
using Phone.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Phone.Application.Tests.Commands.CreateContact;

public class CreateContactCommandHandlerTests
{
    private readonly Mock<IContactRepository> _repo = new();
    private readonly IMapper _mapper;
    private readonly CreateContactCommandHandler _handler;

    public CreateContactCommandHandlerTests()
    {
        var cfg = new MapperConfiguration(c => c.AddProfile<ContactMappingProfile>());
        _mapper = cfg.CreateMapper();
        _handler = new CreateContactCommandHandler(_repo.Object, _mapper);
    }

    [Fact(DisplayName = "Handle should create contact and return DTO")]
    public async Task Handle_ValidCommand_ReturnsContactDto()
    {
        // Arrange
        var command = new CreateContactCommand(
            "João Silva",
            "(11) 99999-9999",
            "joao@email.com",
            null,
            new List<CreateAddressDto>
            {
                new CreateAddressDto{ Logradouro = "Rua A", Numero = "100", Complemento = null, Bairro = "Centro", Cidade = "SP", Estado = "SP", CEP = "01000-000" }
            });

        _repo.Setup(r => r.CreateAsync(It.IsAny<Contact>()))
             .ReturnsAsync((Contact c) => { c.Id = "507f1f77bcf86cd799439011"; return c; });

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be("507f1f77bcf86cd799439011");
        result.Nome.Should().Be(command.Nome);
        _repo.Verify(r => r.CreateAsync(It.IsAny<Contact>()), Times.Once);
    }
}
