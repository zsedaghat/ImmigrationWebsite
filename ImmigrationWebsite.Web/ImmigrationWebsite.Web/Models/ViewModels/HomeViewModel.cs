using ImmigrationWebsite.Web.Models;

namespace ImmigrationWebsite.Web.Models.ViewModels;

public class HomeViewModel
{
    public List<Country> Countries { get; set; } = new();
    public List<Service> Services { get; set; } = new();
}