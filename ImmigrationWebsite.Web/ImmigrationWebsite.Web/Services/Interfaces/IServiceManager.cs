using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Models.Pagination;

namespace ImmigrationWebsite.Web.Services.Interfaces
{
    public interface IServiceManager
    {
        Task<PagedResult<Service>> GetPagedAsync(
     int pageNumber,
     int pageSize);
        Task<Service?> GetByIdAsync(int id);
        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(int id);
    }
}
