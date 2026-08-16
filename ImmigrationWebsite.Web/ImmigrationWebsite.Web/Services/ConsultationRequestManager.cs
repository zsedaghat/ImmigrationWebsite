using ImmigrationWebsite.Web.Data;
using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Models.Pagination;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImmigrationWebsite.Web.Services;

public class ConsultationRequestManager : IConsultationRequestManager
{
    private readonly ApplicationDbContext _context;

    public ConsultationRequestManager(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<ConsultationRequest>> GetPagedAsync(
        int pageNumber,
        int pageSize)
    {
        var query = _context.ConsultationRequests
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.Id);

        var totalItems = await query.CountAsync();

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<ConsultationRequest>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }

    public async Task<ConsultationRequest?> GetByIdAsync(int id)
    {
        return await _context.ConsultationRequests
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(ConsultationRequest request)
    {
        request.CreatedAt = DateTime.UtcNow;

        _context.ConsultationRequests.Add(request);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var request = await GetByIdAsync(id);

        if (request == null)
            return;

        _context.ConsultationRequests.Remove(request);

        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ConsultationRequest request)
    {
        _context.ConsultationRequests.Update(request);

        await _context.SaveChangesAsync();
    }
}