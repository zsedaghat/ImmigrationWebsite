using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CountriesController : Controller
{
    private readonly ICountryService _countryService;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    public async Task<IActionResult> Index(
      int page = 1,
      int pageSize = 10)
    {
        var countries = await _countryService.GetPagedAsync(
            page,
            pageSize);

        return View(countries);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        Country country,
        IFormFile? Image)
    {

        var name = country.Name;
        var description = country.Description;
        var displayOrder = country.DisplayOrder;
        var isActive = country.IsActive;

        if (!ModelState.IsValid)
            return View(country);

        if (Image != null && Image.Length > 0)
        {
            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "countries");

            Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(Image.FileName);

            var filePath = Path.Combine(
                uploadsFolder,
                fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }

            country.ImageUrl = "/uploads/countries/" + fileName;
        }

        await _countryService.AddAsync(country);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var country = await _countryService.GetByIdAsync(id);

        if (country == null)
            return NotFound();

        return View(country);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
    Country country,
    IFormFile? Image)
    {
        if (!ModelState.IsValid)
            return View(country);

        var existingCountry = await _countryService.GetByIdAsync(country.Id);

        if (existingCountry == null)
            return NotFound();

        existingCountry.Name = country.Name;
        existingCountry.Description = country.Description;
        existingCountry.DisplayOrder = country.DisplayOrder;
        existingCountry.IsActive = country.IsActive;

        if (Image != null && Image.Length > 0)
        {
            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "countries");

            Directory.CreateDirectory(uploadsFolder);

            var oldImageUrl = existingCountry.ImageUrl;

            var fileName =
                Guid.NewGuid() + Path.GetExtension(Image.FileName);

            var filePath = Path.Combine(
                uploadsFolder,
                fileName);

            using (var stream = new FileStream(
                filePath,
                FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }

            existingCountry.ImageUrl =
                "/uploads/countries/" + fileName;

            // Delete old image
            if (!string.IsNullOrEmpty(oldImageUrl))
            {
                var oldImagePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    oldImageUrl.TrimStart('/')
                        .Replace('/', Path.DirectorySeparatorChar));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
        }

        await _countryService.UpdateAsync(existingCountry);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var country = await _countryService.GetByIdAsync(id);

        if (country == null)
            return NotFound();

        if (!string.IsNullOrEmpty(country.ImageUrl))
        {
            var imagePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                country.ImageUrl.TrimStart('/')
                    .Replace('/', Path.DirectorySeparatorChar));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        await _countryService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}