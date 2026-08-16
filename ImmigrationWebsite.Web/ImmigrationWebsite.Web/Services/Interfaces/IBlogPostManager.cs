using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Models.Pagination;

namespace ImmigrationWebsite.Web.Services.Interfaces;

public interface IBlogPostManager
{
    Task<PagedResult<BlogPost>> GetPagedAsync(
     int pageNumber,
     int pageSize);
    Task<BlogPost?> GetByIdAsync(int id);
    Task AddAsync(BlogPost blogPost);
    Task UpdateAsync(BlogPost blogPost);
    Task DeleteAsync(int id);
}