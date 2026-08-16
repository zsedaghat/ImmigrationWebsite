namespace ImmigrationWebsite.Web.Models;

public class ConsultationRequest
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Country { get; set; }

    public string? Service { get; set; }

    public string? Message { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = "New";
}