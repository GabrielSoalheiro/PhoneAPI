using FluentAssertions;
using Phone.Domain.Entities;
using System.Collections.Generic;
using Xunit;

namespace Phone.Domain.Tests.Entities;

public class ContactTests
{
    [Fact]
    public void Update_Should_Change_Properties_And_Timestamp()
    {
        var c = new Contact { Nome = "A", Telefone = "T", Email = "E" };
        var before = c.AtualizadoEm;

        c.Update("B", "T2", "E2", new List<Address>(), null);

        c.Nome.Should().Be("B");
        c.Email.Should().Be("E2");
        c.AtualizadoEm.Should().BeAfter(before);
    }
}
