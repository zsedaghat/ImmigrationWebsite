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

    public async Task<IActionResult> Details(int id)
    {
        var service = await _serviceManager.GetByIdAsync(id);

        if (service == null || !service.IsActive)
            return NotFound();

        return View(service);
    }
}