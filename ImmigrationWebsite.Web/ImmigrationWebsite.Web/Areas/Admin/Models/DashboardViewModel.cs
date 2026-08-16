using ImmigrationWebsite.Web.Models;

namespace ImmigrationWebsite.Web.Areas.Admin.Models;

public class DashboardViewModel
{
    public int CountriesCount { get; set; }

    public int ServicesCount { get; set; }

    public int BlogPostsCount { get; set; }

    public int ConsultationRequestsCount { get; set; }

    public List<ConsultationRequest> LatestConsultationRequests { get; set; } = new();

    public int NewConsultationRequestsCount { get; set; }
}