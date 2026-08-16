namespace ImmigrationWebsite.Web.Models;

public class BlogPost
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Summary { get; set; }

    public string Content { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public DateTime PublishedAt { get; set; }

    public bool IsPublished { get; set; } = false;
}