using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoShare.Infrastructure.Data.Users;


namespace PhotoShare.Areas.Identity.Pages.Photgraphers
{
    public class DetailsModel : PageModel
    {
        public PhotographyUser User;
        private readonly UserManager<PhotographyUser> _userManager;

        public DetailsModel(UserManager<PhotographyUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task OnGetAsync(string id)
        {
            if(id != null)
                User = await _userManager.FindByIdAsync(id);
        }
    }
}
