namespace ImmigrationWebsite.Web.Models;

public enum ReviewStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2
}

public class Review
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Country { get; set; }

    public string Comment { get; set; } = null!;

    public int Rating { get; set; }

    public string? ImageUrl { get; set; }

    public ReviewStatus Status { get; set; } = ReviewStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}