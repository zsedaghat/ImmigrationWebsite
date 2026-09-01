using ImmigrationWebsite.Web.Data;
using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImmigrationWebsite.Web.Services;

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext _context;

    public ReviewService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<Review>> GetApprovedReviewsAsync(int count)
    {
        return await _context.Reviews
            .Where(x => x.Status == ReviewStatus.Approved)
            .OrderByDescending(x => x.CreatedAt)
            .Take(count)
            .ToListAsync();
    }


    public async Task AddAsync(Review review)
    {
        await _context.Reviews.AddAsync(review);

        await _context.SaveChangesAsync();
    }



    public async Task<List<Review>> GetAllAsync()
    {
        return await _context.Reviews
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }


    public async Task<Review?> GetByIdAsync(int id)
    {
        return await _context.Reviews
            .FirstOrDefaultAsync(x => x.Id == id);
    }


    public async Task UpdateAsync(Review review)
    {
        _context.Reviews.Update(review);

        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(int id)
    {
        var review = await GetByIdAsync(id);

        if (review != null)
        {
            // Delete image file
            if (!string.IsNullOrEmpty(review.ImageUrl))
            {
                var imagePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    review.ImageUrl.TrimStart('/')
                );


                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }


            // Delete database record
            _context.Reviews.Remove(review);

            await _context.SaveChangesAsync();
        }
    }
}