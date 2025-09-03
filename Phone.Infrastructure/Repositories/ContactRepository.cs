using MongoDB.Driver;
using Phone.Domain.Entities;
using Phone.Domain.Interfaces;

namespace Phone.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly IMongoCollection<Contact> _col;
    public ContactRepository(IMongoDatabase db) => _col = db.GetCollection<Contact>("contatos");

    public async Task<Contact> CreateAsync(Contact c)
    {
        await _col.InsertOneAsync(c);
        return c;
    }

    public async Task<bool> DeleteAsync(string id)
        => (await _col.DeleteOneAsync(x => x.Id == id)).DeletedCount > 0;

    public async Task<IEnumerable<Contact>> GetAllAsync()
        => await _col.Find(_ => true).SortByDescending(x => x.CriadoEm).ToListAsync();

    public async Task<Contact?> GetByIdAsync(string id)
        => await _col.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task<bool> UpdateAsync(string id, Contact c)
        => (await _col.ReplaceOneAsync(x => x.Id == id, c)).ModifiedCount > 0;

    public async Task<IEnumerable<Contact>> SearchAsync(string term)
    {
        if (string.IsNullOrWhiteSpace(term)) return Enumerable.Empty<Contact>();

        var regex = new MongoDB.Bson.BsonRegularExpression(term, "i");
        var f = Builders<Contact>.Filter.Or(
            Builders<Contact>.Filter.Regex(x => x.Nome, regex),
            Builders<Contact>.Filter.Regex(x => x.Email, regex),
            Builders<Contact>.Filter.Regex(x => x.Telefone, regex));

        return await _col.Find(f).ToListAsync();
    }

    public async Task<bool> ExistsByEmailAsync(string email, string? excludeId = null)
    {
        var filter = Builders<Contact>.Filter.Eq(x => x.Email, email);
        if (excludeId != null) filter &= Builders<Contact>.Filter.Ne(x => x.Id, excludeId);
        return await _col.CountDocumentsAsync(filter) > 0;
    }

    public async Task<bool> ExistsByTelefoneAsync(string phone, string? excludeId = null)
    {
        var filter = Builders<Contact>.Filter.Eq(x => x.Telefone, phone);
        if (excludeId != null) filter &= Builders<Contact>.Filter.Ne(x => x.Id, excludeId);
        return await _col.CountDocumentsAsync(filter) > 0;
    }
}
