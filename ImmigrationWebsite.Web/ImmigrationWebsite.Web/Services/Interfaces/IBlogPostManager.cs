using ImmigrationWebsite.Web.Models;

namespace ImmigrationWebsite.Web.Services.Interfaces;

public interface IBlogPostManager
{
    Task<List<BlogPost>> GetAllAsync();
    Task<BlogPost?> GetByIdAsync(int id);
    Task AddAsync(BlogPost blogPost);
    Task UpdateAsync(BlogPost blogPost);
    Task DeleteAsync(int id);
}