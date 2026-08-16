using ImmigrationWebsite.Web.Data;
using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImmigrationWebsite.Web.Services;

public class ContactMessageManager : IContactMessageManager
{
    private readonly ApplicationDbContext _context;

    public ContactMessageManager(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ContactMessage>> GetAllAsync()
    {
        return await _context.ContactMessages
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<ContactMessage?> GetByIdAsync(int id)
    {
        return await _context.ContactMessages
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(ContactMessage message)
    {
        message.CreatedAt = DateTime.UtcNow;

        _context.ContactMessages.Add(message);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var message = await GetByIdAsync(id);

        if (message == null)
            return;

        _context.ContactMessages.Remove(message);

        await _context.SaveChangesAsync();
    }
}