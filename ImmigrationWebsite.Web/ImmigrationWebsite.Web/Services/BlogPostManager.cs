using ImmigrationWebsite.Web.Data;
using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Models.Pagination;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImmigrationWebsite.Web.Services;

public class BlogPostManager : IBlogPostManager
{
    private readonly ApplicationDbContext _context;

    public BlogPostManager(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<BlogPost>> GetPagedAsync(
        int pageNumber,
        int pageSize)
    {
        var query = _context.BlogPosts
            .AsNoTracking()
            .OrderByDescending(x => x.PublishedAt)
            .ThenByDescending(x => x.Id);

        var totalItems = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<BlogPost>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }

    public async Task<BlogPost?> GetByIdAsync(int id)
    {
        return await _context.BlogPosts
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(BlogPost blogPost)
    {
        _context.BlogPosts.Add(blogPost);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(BlogPost blogPost)
    {
        _context.BlogPosts.Update(blogPost);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var blogPost = await GetByIdAsync(id);

        if (blogPost == null)
            return;

        _context.BlogPosts.Remove(blogPost);
        await _context.SaveChangesAsync();
    }
}