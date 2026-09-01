using ImmigrationWebsite.Web.Models;

namespace ImmigrationWebsite.Web.Services.Interfaces;

public interface IReviewService
{
    Task<List<Review>> GetApprovedReviewsAsync(int count);

    Task AddAsync(Review review);

    Task<List<Review>> GetAllAsync();

    Task<Review?> GetByIdAsync(int id);

    Task UpdateAsync(Review review);

    Task DeleteAsync(int id);
}