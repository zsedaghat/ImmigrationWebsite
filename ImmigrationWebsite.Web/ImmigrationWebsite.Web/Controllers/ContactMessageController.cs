using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Controllers;

[Authorize(Roles = "Admin")]
public class ContactMessageController : Controller
{
    private readonly IContactMessageManager _messageManager;

    public ContactMessageController(IContactMessageManager messageManager)
    {
        _messageManager = messageManager;
    }

    // GET: /ContactMessage
    public async Task<IActionResult> Index()
    {
        var messages = await _messageManager.GetAllAsync();

        return View(messages);
    }

    // GET: /ContactMessage/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var message = await _messageManager.GetByIdAsync(id);

        if (message == null)
            return NotFound();

        return View(message);
    }

    // POST: /ContactMessage/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ContactMessage message)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _messageManager.AddAsync(message);

        return Ok();
    }

    // POST: /ContactMessage/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _messageManager.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}