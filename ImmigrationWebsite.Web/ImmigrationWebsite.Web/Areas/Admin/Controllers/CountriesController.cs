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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        Country country,
        IFormFile? Image,
        IFormFile? FlagImage)
    {
        if (!ModelState.IsValid)
            return View(country);

        var uploadsFolder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "uploads",
            "countries");

        Directory.CreateDirectory(uploadsFolder);

        // Country Image
        if (Image != null && Image.Length > 0)
        {
            var fileName =
                Guid.NewGuid() + Path.GetExtension(Image.FileName);

            var filePath = Path.Combine(
                uploadsFolder,
                fileName);

            using var stream = new FileStream(
                filePath,
                FileMode.Create);

            await Image.CopyToAsync(stream);

            country.ImageUrl =
                "/uploads/countries/" + fileName;
        }

        // Country Flag
        if (FlagImage != null && FlagImage.Length > 0)
        {
            var fileName =
                Guid.NewGuid() + Path.GetExtension(FlagImage.FileName);

            var filePath = Path.Combine(
                uploadsFolder,
                fileName);

            using var stream = new FileStream(
                filePath,
                FileMode.Create);

            await FlagImage.CopyToAsync(stream);

            country.FlagImageUrl =
                "/uploads/countries/" + fileName;
        }

        await _countryService.AddAsync(country);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
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
        IFormFile? Image,
        IFormFile? FlagImage)
    {
        if (!ModelState.IsValid)
            return View(country);

        var existingCountry =
            await _countryService.GetByIdAsync(country.Id);

        if (existingCountry == null)
            return NotFound();

        existingCountry.Name = country.Name;
        existingCountry.Description = country.Description;
        existingCountry.DisplayOrder = country.DisplayOrder;
        existingCountry.IsActive = country.IsActive;

        var uploadsFolder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "uploads",
            "countries");

        Directory.CreateDirectory(uploadsFolder);

        // =========================
        // Country Image
        // =========================

        if (Image != null && Image.Length > 0)
        {
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

        // =========================
        // Country Flag
        // =========================

        if (FlagImage != null && FlagImage.Length > 0)
        {
            var oldFlagUrl = existingCountry.FlagImageUrl;

            var fileName =
                Guid.NewGuid() + Path.GetExtension(FlagImage.FileName);

            var filePath = Path.Combine(
                uploadsFolder,
                fileName);

            using (var stream = new FileStream(
                filePath,
                FileMode.Create))
            {
                await FlagImage.CopyToAsync(stream);
            }

            existingCountry.FlagImageUrl =
                "/uploads/countries/" + fileName;

            // Delete old flag
            if (!string.IsNullOrEmpty(oldFlagUrl))
            {
                var oldFlagPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    oldFlagUrl.TrimStart('/')
                        .Replace('/', Path.DirectorySeparatorChar));

                if (System.IO.File.Exists(oldFlagPath))
                {
                    System.IO.File.Delete(oldFlagPath);
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

        // Delete Country Image
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

        // Delete Country Flag
        if (!string.IsNullOrEmpty(country.FlagImageUrl))
        {
            var flagPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                country.FlagImageUrl.TrimStart('/')
                    .Replace('/', Path.DirectorySeparatorChar));

            if (System.IO.File.Exists(flagPath))
            {
                System.IO.File.Delete(flagPath);
            }
        }

        await _countryService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}