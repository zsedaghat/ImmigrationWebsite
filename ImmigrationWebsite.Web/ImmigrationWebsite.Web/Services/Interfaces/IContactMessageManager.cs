using ImmigrationWebsite.Web.Models;

namespace ImmigrationWebsite.Web.Services.Interfaces;

public interface IContactMessageManager
{
    Task<List<ContactMessage>> GetAllAsync();
    Task<ContactMessage?> GetByIdAsync(int id);
    Task AddAsync(ContactMessage message);
    Task DeleteAsync(int id);
}