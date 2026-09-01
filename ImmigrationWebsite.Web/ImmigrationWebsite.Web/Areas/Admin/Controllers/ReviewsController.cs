using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ReviewsController : Controller
{
    private readonly IReviewService _reviewService;


    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }


    public async Task<IActionResult> Index()
    {
        var reviews = await _reviewService.GetAllAsync();

        return View(reviews);
    }



    [HttpPost]
    public async Task<IActionResult> Approve(int id)
    {
        var review = await _reviewService.GetByIdAsync(id);

        if (review == null)
            return NotFound();


        review.Status = ReviewStatus.Approved;

        await _reviewService.UpdateAsync(review);


        return RedirectToAction(nameof(Index));
    }



    [HttpPost]
    public async Task<IActionResult> Reject(int id)
    {
        var review = await _reviewService.GetByIdAsync(id);

        if (review == null)
            return NotFound();


        review.Status = ReviewStatus.Rejected;

        await _reviewService.UpdateAsync(review);


        return RedirectToAction(nameof(Index));
    }



    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _reviewService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}