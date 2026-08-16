using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Controllers;

[Authorize(Roles = "Admin")]
public class ConsultationRequestController : Controller
{
    private readonly IConsultationRequestManager _requestManager;

    public ConsultationRequestController(
        IConsultationRequestManager requestManager)
    {
        _requestManager = requestManager;
    }

    // GET: /ConsultationRequest
    public async Task<IActionResult> Index(int page = 1,
        int pageSize = 10)
    {
        var requests = await _requestManager.GetPagedAsync(page,pageSize);

        return View(requests);
    }

    // GET: /ConsultationRequest/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var request = await _requestManager.GetByIdAsync(id);

        if (request == null)
            return NotFound();

        return View(request);
    }

    // POST: /ConsultationRequest/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ConsultationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _requestManager.AddAsync(request);

        return Ok();
    }

    // POST: /ConsultationRequest/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _requestManager.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}