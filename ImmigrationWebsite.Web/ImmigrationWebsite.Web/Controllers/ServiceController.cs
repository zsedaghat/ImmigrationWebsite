using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Controllers;

public class ServicesController : Controller
{
    private readonly IServiceManager _serviceManager;

    public ServicesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var result = await _serviceManager.GetPagedAsync(page, 6);

        return View(result);
    }

    // Serviceهای اصلی با Id
    [HttpGet("/services/{id:int}")]
    public async Task<IActionResult> DetailsById(int id)
    {
        var service = await _serviceManager.GetByIdAsync(id);

        if (service == null || !service.IsActive)
            return NotFound();

        return View("Details", service);
    }

    // فقط Canada / Travel Visas / Education / Resources با Slug
    [HttpGet("/services/{slug}")]
    public async Task<IActionResult> DetailsBySlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            return NotFound();

        var service = await _serviceManager.GetBySlugAsync(slug);

        if (service == null || !service.IsActive)
            return NotFound();

        return View("Details", service);
    }
}