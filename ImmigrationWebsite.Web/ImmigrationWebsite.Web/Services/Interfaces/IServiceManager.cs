using ImmigrationWebsite.Web.Models;

namespace ImmigrationWebsite.Web.Services.Interfaces
{
    public interface IServiceManager
    {
        Task<List<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(int id);
    }
}
