using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Controllers;

[Authorize(Roles = "Admin")]
public class ServiceController : Controller
{
    private readonly IServiceManager _serviceManager;

    public ServiceController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    // GET: /Service
    public async Task<IActionResult> Index(int page = 1,
          int pageSize = 10)
    {
        var services = await _serviceManager.GetPagedAsync(page,pageSize);

        return View(services);
    }

    // GET: /Service/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var service = await _serviceManager.GetByIdAsync(id);

        if (service == null)
            return NotFound();

        return View(service);
    }

    // GET: /Service/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Service/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Service service)
    {
        if (!ModelState.IsValid)
            return View(service);

        await _serviceManager.AddAsync(service);

        return RedirectToAction(nameof(Index));
    }

    // GET: /Service/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var service = await _serviceManager.GetByIdAsync(id);

        if (service == null)
            return NotFound();

        return View(service);
    }

    // POST: /Service/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Service service)
    {
        if (id != service.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(service);

        var existingService = await _serviceManager.GetByIdAsync(id);

        if (existingService == null)
            return NotFound();

        existingService.Title = service.Title;
        existingService.Description = service.Description;
        existingService.ImageUrl = service.ImageUrl;
        existingService.IsActive = service.IsActive;
        existingService.DisplayOrder = service.DisplayOrder;

        await _serviceManager.UpdateAsync(existingService);

        return RedirectToAction(nameof(Index));
    }

    // POST: /Service/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _serviceManager.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}