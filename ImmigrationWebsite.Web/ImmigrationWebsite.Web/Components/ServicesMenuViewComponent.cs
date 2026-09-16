using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.ViewComponents
{
    public class ServicesMenuViewComponent : ViewComponent
    {
        private readonly IServiceManager _serviceManager;

        public ServicesMenuViewComponent(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _serviceManager.GetPagedAsync(1, 100);

            var excludedSlugs = new[]
            {
                "canada",
                "travel-visas",
                "education",
                "resources"
            };

            var services = result.Items
                .Where(x => x.IsActive &&
                            !excludedSlugs.Contains(x.Slug))
                .OrderBy(x => x.DisplayOrder)
                .ToList();

            return View(services);
        }
    }
}