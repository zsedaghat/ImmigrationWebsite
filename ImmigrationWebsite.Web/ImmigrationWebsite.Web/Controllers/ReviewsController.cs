using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Models.ViewModels;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Controllers;

public class ReviewsController : Controller
{
    private readonly IReviewService _reviewService;
    private readonly IWebHostEnvironment _environment;


    public ReviewsController(
        IReviewService reviewService,
        IWebHostEnvironment environment)
    {
        _reviewService = reviewService;
        _environment = environment;
    }


    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReviewCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);


        string? imageUrl = null;


        // Upload Image
        if (model.Image != null)
        {
            var folderPath = Path.Combine(
                _environment.WebRootPath,
                "uploads",
                "reviews");


            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }


            var fileName = Guid.NewGuid()
                + Path.GetExtension(model.Image.FileName);


            var filePath = Path.Combine(
                folderPath,
                fileName);


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.Image.CopyToAsync(stream);
            }


            imageUrl = "/uploads/reviews/" + fileName;
        }



        var review = new Review
        {
            Name = model.Name,

            Country = model.Country,

            Comment = model.Comment,

            Rating = model.Rating,

            ImageUrl = imageUrl,

            Status = ReviewStatus.Pending,

            CreatedAt = DateTime.UtcNow
        };


        await _reviewService.AddAsync(review);


        return RedirectToAction(nameof(Success));
    }



    [HttpGet]
    public IActionResult Success()
    {
        return View();
    }
}