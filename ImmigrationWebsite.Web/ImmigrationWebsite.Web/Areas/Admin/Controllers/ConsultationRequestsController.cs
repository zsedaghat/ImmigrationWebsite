using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ConsultationRequestsController : Controller
{
    private readonly IConsultationRequestManager _requestManager;

    public ConsultationRequestsController(
        IConsultationRequestManager requestManager)
    {
        _requestManager = requestManager;
    }

    // GET: /Admin/ConsultationRequests
    public async Task<IActionResult> Index(
        int page = 1,
        int pageSize = 10)
    {
        var requests = await _requestManager.GetPagedAsync(
            page,
            pageSize);

        return View(requests);
    }

    // GET: /Admin/ConsultationRequests/Details/5
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var request = await _requestManager.GetByIdAsync(id);

        if (request == null)
            return NotFound();

        return View(request);
    }

    // POST: /Admin/ConsultationRequests/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _requestManager.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }

    // GET: /Admin/ConsultationRequests/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var request = await _requestManager.GetByIdAsync(id);

        if (request == null)
            return NotFound();

        return View(request);
    }

    // POST: /Admin/ConsultationRequests/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        string status)
    {
        var request = await _requestManager.GetByIdAsync(id);

        if (request == null)
            return NotFound();

        request.Status = status;

        await _requestManager.UpdateAsync(request);

        return RedirectToAction(nameof(Details), new { id = request.Id });
    }
}