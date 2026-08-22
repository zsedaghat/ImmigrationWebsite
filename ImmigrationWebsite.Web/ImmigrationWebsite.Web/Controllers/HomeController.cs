using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Models.ViewModels;
using ImmigrationWebsite.Web.Services;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ImmigrationWebsite.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICountryService _countryService;
        private readonly IServiceManager _serviceManager;


        public HomeController(
            ILogger<HomeController> logger,
            ICountryService countryService,
            IServiceManager serviceManager)
        {
            _logger = logger;
            _countryService = countryService;
            _serviceManager = serviceManager;
        }

        public async Task<IActionResult> Index()
        {
            var countries = await _countryService.GetPagedAsync(1, 4);

            countries.Items = countries.Items
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ToList();

            var services = await _serviceManager.GetPagedAsync(1, 6);

            services.Items = services.Items
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ToList();

            var model = new HomeViewModel
            {
                Countries = countries.Items,
                Services = services.Items
            };

            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}