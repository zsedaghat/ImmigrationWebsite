using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Models.Pagination;

namespace ImmigrationWebsite.Web.Services.Interfaces
{
    public interface ICountryService
    {
        Task<PagedResult<Country>> GetPagedAsync(int pageNumber, int pageSize);
        Task<Country?> GetByIdAsync(int id);
        Task AddAsync(Country country);
        Task UpdateAsync(Country country);
        Task DeleteAsync(int id);
    }
}