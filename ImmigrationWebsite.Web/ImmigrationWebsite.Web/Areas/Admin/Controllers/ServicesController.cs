using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicesController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public ServicesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: /Admin/Services
        public async Task<IActionResult> Index(
          int page = 1,
          int pageSize = 10)
        {
            var services = await _serviceManager.GetPagedAsync(
                page,
                pageSize);

            return View(services);
        }

        // GET: /Admin/Services/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Services/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Service service, IFormFile? image)
        {
            if (!ModelState.IsValid)
                return View(service);

            if (image != null && image.Length > 0)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads/services"
                );

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                service.ImageUrl = "/uploads/services/" + fileName;
            }

            await _serviceManager.AddAsync(service);

            return RedirectToAction(nameof(Index));
        }

        // GET: /Admin/Services/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var service = await _serviceManager.GetByIdAsync(id);

            if (service == null)
                return NotFound();

            return View(service);
        }

        // POST: /Admin/Services/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Service service, IFormFile? image)
        {
            if (!ModelState.IsValid)
                return View(service);

            var existingService = await _serviceManager.GetByIdAsync(service.Id);

            if (existingService == null)
                return NotFound();

            existingService.Title = service.Title;
            existingService.Description = service.Description;
            existingService.IsActive = service.IsActive;
            existingService.DisplayOrder = service.DisplayOrder;

            if (image != null && image.Length > 0)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads/services"
                );

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                existingService.ImageUrl = "/uploads/services/" + fileName;
            }

            await _serviceManager.UpdateAsync(existingService);

            return RedirectToAction(nameof(Index));
        }

        // POST: /Admin/Services/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceManager.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}