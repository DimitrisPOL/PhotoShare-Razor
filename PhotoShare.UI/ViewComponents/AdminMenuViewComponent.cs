using Microsoft.AspNetCore.Mvc;
using PhotoShare.Views.Shared.Components.AdminMenu;

namespace PhotoShare.ViewComponents
{
    public class AdminMenuViewComponent : ViewComponent
    {
        public AdminMenuViewComponent(){}

        public async Task<IViewComponentResult> InvokeAsync(AdminMenuViewModel model)
        {
            return View(model);
        }

    }
}
