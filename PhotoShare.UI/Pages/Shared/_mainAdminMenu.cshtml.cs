using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhotoShare.Pages.Shared
{
    public class _mainAdminMenuModel : PageModel
    {
        
        public string _originComponent;

        public _mainAdminMenuModel(string originComponent)
        {
            _originComponent = originComponent;
        }

        public void OnGet()
        {
        }
    }
}
