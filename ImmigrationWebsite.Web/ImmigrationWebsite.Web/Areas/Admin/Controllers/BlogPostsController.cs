using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class BlogPostsController : Controller
{
    private readonly IBlogPostManager _blogPostManager;

    public BlogPostsController(IBlogPostManager blogPostManager)
    {
        _blogPostManager = blogPostManager;
    }

    public async Task<IActionResult> Index(
        int page = 1,
        int pageSize = 10)
    {
        var posts = await _blogPostManager.GetPagedAsync(
            page,
            pageSize);

        return View(posts);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        BlogPost blogPost,
        IFormFile? image)
    {
        if (!ModelState.IsValid)
            return View(blogPost);

        if (image != null && image.Length > 0)
        {
            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "blog");

            Directory.CreateDirectory(uploadsFolder);

            var fileName =
                Guid.NewGuid() +
                Path.GetExtension(image.FileName);

            var filePath = Path.Combine(
                uploadsFolder,
                fileName);

            using (var stream = new FileStream(
                filePath,
                FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            blogPost.ImageUrl =
                "/uploads/blog/" + fileName;
        }

        if (blogPost.PublishedAt == default)
        {
            blogPost.PublishedAt = DateTime.Now;
        }

        await _blogPostManager.AddAsync(blogPost);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var blogPost =
            await _blogPostManager.GetByIdAsync(id);

        if (blogPost == null)
            return NotFound();

        return View(blogPost);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        BlogPost blogPost,
        IFormFile? image)
    {
        if (!ModelState.IsValid)
            return View(blogPost);

        var existingPost =
            await _blogPostManager.GetByIdAsync(blogPost.Id);

        if (existingPost == null)
            return NotFound();

        existingPost.Title = blogPost.Title;
        existingPost.Summary = blogPost.Summary;
        existingPost.Content = blogPost.Content;
        existingPost.PublishedAt = blogPost.PublishedAt;
        existingPost.IsPublished = blogPost.IsPublished;

        if (image != null && image.Length > 0)
        {
            var uploadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "uploads",
                "blog");

            Directory.CreateDirectory(uploadsFolder);

            var oldImageUrl = existingPost.ImageUrl;

            var fileName =
                Guid.NewGuid() +
                Path.GetExtension(image.FileName);

            var filePath = Path.Combine(
                uploadsFolder,
                fileName);

            using (var stream = new FileStream(
                filePath,
                FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            existingPost.ImageUrl =
                "/uploads/blog/" + fileName;

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

        await _blogPostManager.UpdateAsync(existingPost);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var blogPost =
            await _blogPostManager.GetByIdAsync(id);

        if (blogPost == null)
            return NotFound();

        if (!string.IsNullOrEmpty(blogPost.ImageUrl))
        {
            var imagePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                blogPost.ImageUrl
                    .TrimStart('/')
                    .Replace(
                        '/',
                        Path.DirectorySeparatorChar));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        await _blogPostManager.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}