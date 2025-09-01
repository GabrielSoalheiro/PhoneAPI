using AutoMapper;
using FluentAssertions;
using Moq;
using Phone.Domain.Entities;
using Phone.Domain.Interfaces;
using Phone.Application.Mappings;
using Phone.Application.DTOs;
using Phone.Application.Mappings;
using Phone.Application.Queries.GetAllContacts;
using Phone.Domain.Entities;
using Phone.Domain.Interfaces;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Phone.Application.Tests.Queries.GetAllContacts;

public class GetAllContactsQueryHandlerTests
{
    private readonly Mock<IContactRepository> _repo = new();
    private readonly IMapper _mapper;
    private readonly GetAllContactsQueryHandler _handler;

    public GetAllContactsQueryHandlerTests()
    {
        _mapper = new MapperConfiguration(c => c.AddProfile<ContactMappingProfile>()).CreateMapper();
        _handler = new GetAllContactsQueryHandler(_repo.Object, _mapper);
    }

    [Fact]
    public async Task Handle_Returns_List()
    {
        // Arrange
        var list = new List<Contact> { new() { Nome = "Teste", Telefone = "(11) 99999-9999", Email = "t@e.com" } };
        _repo.Setup(r => r.GetAllAsync()).ReturnsAsync(list);

        // Act
        var res = await _handler.Handle(new GetAllContactsQuery(), default);

        // Assert
        res.Should().HaveCount(1);
        res.First().Nome.Should().Be("Teste");
    }
}
