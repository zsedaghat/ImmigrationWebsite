using ImmigrationWebsite.Web.Models;
using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Controllers;

[Authorize(Roles = "Admin")]
public class BlogPostController : Controller
{
    private readonly IBlogPostManager _blogPostManager;

    public BlogPostController(IBlogPostManager blogPostManager)
    {
        _blogPostManager = blogPostManager;
    }

    // GET: /BlogPost
    public async Task<IActionResult> Index()
    {
        var posts = await _blogPostManager.GetAllAsync();

        return View(posts);
    }

    // GET: /BlogPost/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var post = await _blogPostManager.GetByIdAsync(id);

        if (post == null)
            return NotFound();

        return View(post);
    }

    // GET: /BlogPost/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: /BlogPost/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BlogPost blogPost)
    {
        if (!ModelState.IsValid)
            return View(blogPost);

        await _blogPostManager.AddAsync(blogPost);

        return RedirectToAction(nameof(Index));
    }

    // GET: /BlogPost/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var post = await _blogPostManager.GetByIdAsync(id);

        if (post == null)
            return NotFound();

        return View(post);
    }

    // POST: /BlogPost/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, BlogPost blogPost)
    {
        if (id != blogPost.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(blogPost);

        var existingPost = await _blogPostManager.GetByIdAsync(id);

        if (existingPost == null)
            return NotFound();

        existingPost.Title = blogPost.Title;
        existingPost.Summary = blogPost.Summary;
        existingPost.Content = blogPost.Content;
        existingPost.ImageUrl = blogPost.ImageUrl;
        existingPost.PublishedAt = blogPost.PublishedAt;
        existingPost.IsPublished = blogPost.IsPublished;

        await _blogPostManager.UpdateAsync(existingPost);

        return RedirectToAction(nameof(Index));
    }

    // POST: /BlogPost/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _blogPostManager.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}