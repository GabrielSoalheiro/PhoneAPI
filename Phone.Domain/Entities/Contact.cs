using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Phone.Domain.Entities;

public class Contact
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("nome")] public string Nome { get; set; } = string.Empty;
    [BsonElement("telefone")] public string Telefone { get; set; } = string.Empty;
    [BsonElement("email")] public string Email { get; set; } = string.Empty;
    [BsonElement("dataNascimento")] public DateTime? DataNascimento { get; set; }

    [BsonElement("enderecos")] public List<Address> Enderecos { get; set; } = new();

    [BsonElement("criadoEm")] public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    [BsonElement("atualizadoEm")] public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;

    public void Update(string nome, string telefone, string email, List<Address> enderecos, DateTime? dn)
    {
        Nome = nome; Telefone = telefone; Email = email; Enderecos = enderecos; DataNascimento = dn;
        AtualizadoEm = DateTime.UtcNow;
    }
}
