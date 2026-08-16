using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
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

        public async Task<IActionResult> Details(int id)
        {
            var country = await _countryService.GetByIdAsync(id);

            if (country == null)
                return NotFound();

            return View(country);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (!ModelState.IsValid)
                return View(country);

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
        public async Task<IActionResult> Edit(int id, Country country)
        {
            if (id != country.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(country);

            var existingCountry = await _countryService.GetByIdAsync(id);

            if (existingCountry == null)
                return NotFound();

            existingCountry.Name = country.Name;
            //existingCountry.Slug = country.Slug;
            //existingCountry.ShortDescription = country.ShortDescription;
            existingCountry.Description = country.Description;
            //existingCountry.Image = country.Image;
            existingCountry.IsActive = country.IsActive;

            await _countryService.UpdateAsync(existingCountry);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _countryService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}