using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ServicesController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public ServicesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index(
            int page = 1,
            int pageSize = 10)
        {
            var services = await _serviceManager.GetPagedAsync(
                page,
                pageSize);

            return View(services);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Service service,
            IFormFile? image)
        {
            if (!ModelState.IsValid)
                return View(service);

            if (image != null && image.Length > 0)
            {
                var uploadsFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "uploads",
                    "services");

                Directory.CreateDirectory(uploadsFolder);

                var fileName =
                    Guid.NewGuid() + Path.GetExtension(image.FileName);

                var filePath = Path.Combine(
                    uploadsFolder,
                    fileName);

                using (var stream = new FileStream(
                    filePath,
                    FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                service.ImageUrl =
                    "/uploads/services/" + fileName;
            }

            await _serviceManager.AddAsync(service);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var service = await _serviceManager.GetByIdAsync(id);

            if (service == null)
                return NotFound();

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            Service service,
            IFormFile? image)
        {
            if (!ModelState.IsValid)
                return View(service);

            var existingService =
                await _serviceManager.GetByIdAsync(service.Id);

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
                    "wwwroot",
                    "uploads",
                    "services");

                Directory.CreateDirectory(uploadsFolder);

                var oldImageUrl = existingService.ImageUrl;

                var fileName =
                    Guid.NewGuid() + Path.GetExtension(image.FileName);

                var filePath = Path.Combine(
                    uploadsFolder,
                    fileName);

                using (var stream = new FileStream(
                    filePath,
                    FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                existingService.ImageUrl =
                    "/uploads/services/" + fileName;

                if (!string.IsNullOrEmpty(oldImageUrl))
                {
                    var oldImagePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        oldImageUrl
                            .TrimStart('/')
                            .Replace(
                                '/',
                                Path.DirectorySeparatorChar));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
            }

            await _serviceManager.UpdateAsync(existingService);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var service =
                await _serviceManager.GetByIdAsync(id);

            if (service == null)
                return NotFound();

            if (!string.IsNullOrEmpty(service.ImageUrl))
            {
                var imagePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    service.ImageUrl
                        .TrimStart('/')
                        .Replace(
                            '/',
                            Path.DirectorySeparatorChar));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            await _serviceManager.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}