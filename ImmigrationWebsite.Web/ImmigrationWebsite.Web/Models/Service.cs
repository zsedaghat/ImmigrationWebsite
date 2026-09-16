namespace ImmigrationWebsite.Web.Models;

public class Service
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    //public ServiceType? Type { get; set; }
    public string? Slug { get; set; } 

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public int DisplayOrder { get; set; }
}