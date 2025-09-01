using MongoDB.Driver;
using Phone.Domain.Entities;

namespace Phone.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _db;
    public MongoDbContext(IMongoDatabase db) => _db = db;

    public IMongoCollection<Contact> Contacts => _db.GetCollection<Contact>("contatos");
}
