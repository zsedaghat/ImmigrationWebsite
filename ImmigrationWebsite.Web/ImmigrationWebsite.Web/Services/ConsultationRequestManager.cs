using ImmigrationWebsite.Web.Data;
using ImmigrationWebsite.Web.Models;
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

    public async Task<List<ConsultationRequest>> GetAllAsync()
    {
        return await _context.ConsultationRequests
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
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
}