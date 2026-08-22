using ImmigrationWebsite.Web.Data;
using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Models.Pagination;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImmigrationWebsite.Web.Services
{
    public class CountryService : ICountryService
    {
        private readonly ApplicationDbContext _context;

        public CountryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Country>> GetPagedAsync(
       int pageNumber,
       int pageSize)
        {
            var query = _context.Countries
                .AsNoTracking()
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.Id);

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Country>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

        public async Task<Country?> GetByIdAsync(int id)
        {
            return await _context.Countries
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Country country)
        {
            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var country = await GetByIdAsync(id);

            if (country == null)
                return;

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
        }
    }
}