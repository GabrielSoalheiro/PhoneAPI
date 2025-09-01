using MongoDB.Bson.Serialization.Attributes;

namespace Phone.Domain.Entities;

public class Address
{
    [BsonElement("logradouro")] public string Logradouro { get; set; } = string.Empty;
    [BsonElement("numero")] public string Numero { get; set; } = string.Empty;
    [BsonElement("complemento")] public string? Complemento { get; set; }
    [BsonElement("bairro")] public string Bairro { get; set; } = string.Empty;
    [BsonElement("cidade")] public string Cidade { get; set; } = string.Empty;
    [BsonElement("estado")] public string Estado { get; set; } = string.Empty;
    [BsonElement("cep")] public string CEP { get; set; } = string.Empty;
    [BsonElement("pais")] public string Pais { get; set; } = "Brasil";
}
