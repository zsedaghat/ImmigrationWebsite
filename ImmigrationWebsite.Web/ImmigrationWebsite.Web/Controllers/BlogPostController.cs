using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Controllers;

public class BlogController : Controller
{
    private readonly IBlogPostManager _blogPostManager;

    public BlogController(IBlogPostManager blogPostManager)
    {
        _blogPostManager = blogPostManager;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var result = await _blogPostManager.GetPagedAsync(page, 6);

        // فقط پست‌های منتشر شده
        result.Items = result.Items
            .Where(x => x.IsPublished)
            .ToList();

        return View(result);
    }

    public async Task<IActionResult> Details(int id)
    {
        var blogPost = await _blogPostManager.GetByIdAsync(id);

        if (blogPost == null || !blogPost.IsPublished)
            return NotFound();

        return View(blogPost);
    }
}