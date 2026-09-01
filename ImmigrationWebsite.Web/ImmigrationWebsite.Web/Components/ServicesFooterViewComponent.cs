using ImmigrationWebsite.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImmigrationWebsite.Web.Components;

public class ServicesFooterViewComponent : ViewComponent
{
    private readonly IServiceManager _serviceManager;

    public ServicesFooterViewComponent(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await _serviceManager.GetPagedAsync(1, 6);

        return View(result.Items);
    }
}