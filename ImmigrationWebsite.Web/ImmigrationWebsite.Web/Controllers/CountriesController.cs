using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Controllers;

public class CountriesController : Controller
{
    private readonly ICountryService _countryService;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var result = await _countryService.GetPagedAsync(page, 8);

        return View(result);
    }
    public async Task<IActionResult> Details(int id)
    {
        var country = await _countryService.GetByIdAsync(id);

        if (country == null || !country.IsActive)
            return NotFound();

        return View(country);
    }
}