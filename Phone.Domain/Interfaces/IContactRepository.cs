using Phone.Domain.Entities;

namespace Phone.Domain.Interfaces;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetAllAsync();
    Task<Contact?> GetByIdAsync(string id);
    Task<Contact> CreateAsync(Contact c);
    Task<bool> UpdateAsync(string id, Contact c);
    Task<bool> DeleteAsync(string id);
    Task<IEnumerable<Contact>> SearchAsync(string term);

    Task<bool> ExistsByEmailAsync(string email, string? excludeId = null);
    Task<bool> ExistsByTelefoneAsync(string phone, string? excludeId = null);
}
