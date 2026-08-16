using ImmigrationWebsite.Web.Areas.Admin.Models;
using ImmigrationWebsite.Web.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImmigrationWebsite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var viewModel = new DashboardViewModel
        {
            CountriesCount = await _context.Countries.CountAsync(),

            ServicesCount = await _context.Services.CountAsync(),

            BlogPostsCount = await _context.BlogPosts.CountAsync(),

            ConsultationRequestsCount =
                await _context.ConsultationRequests.CountAsync(),

            NewConsultationRequestsCount =
    await _context.ConsultationRequests
        .CountAsync(x => x.Status == "New"),

            LatestConsultationRequests =
                await _context.ConsultationRequests
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(5)
                    .ToListAsync()
        };

        return View(viewModel);
    }
}