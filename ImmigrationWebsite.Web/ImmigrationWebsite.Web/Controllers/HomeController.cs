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
        private readonly IConsultationRequestManager _consultationRequestManager;
        private readonly IReviewService _reviewService;



        public HomeController(
            ILogger<HomeController> logger,
            ICountryService countryService,
            IServiceManager serviceManager,
            IConsultationRequestManager consultationRequestManager,
            IReviewService reviewService)
        {
            _logger = logger;
            _countryService = countryService;
            _serviceManager = serviceManager;
            _consultationRequestManager = consultationRequestManager;
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            var countries = await _countryService.GetPagedAsync(1, 4);

            countries.Items = countries.Items
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ToList();


            var services = await _serviceManager.GetPagedAsync(1, 10);

            services.Items = services.Items
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ToList();


            var reviews = await _reviewService.GetApprovedReviewsAsync(6);


            var model = new HomeViewModel
            {
                Countries = countries.Items,
                Services = services.Items,
                Reviews = reviews
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

        [HttpGet]
        public IActionResult Contact()
        {
            return View(new ConsultationRequest());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ConsultationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            await _consultationRequestManager.AddAsync(request);

            TempData["SuccessMessage"] =
                "Your consultation request has been submitted successfully.";

            return RedirectToAction(nameof(Contact));
        }

        [HttpGet("/privacy-policy")]
        public IActionResult PrivacyPolicy()
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

        [HttpGet("/disclaimer")]
        public IActionResult Disclaimer()
        {
            return View();
        }


    }
}