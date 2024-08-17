using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

using PhotoShare.Data;
using PhotoShare.Infrastructure.Data.Users;


namespace PhotoShare.Pages.PhotographersAdminPage
{
    [Authorize(Roles = "admin")]
    public class IndexModel : PageModel
    {
        public List<PhotographyUser> Users;
        private readonly UserManager<PhotographyUser> _userManager;
        public RoleManager<IdentityRole> _rolemanager;
        public IndexModel(UserManager<PhotographyUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _rolemanager = roleManager;
            Users = new List<PhotographyUser>();
        }

        public async Task<IActionResult> OnGetEnableToggle(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.SetLockoutEnabledAsync(user, !user.LockoutEnabled);

            return RedirectToPage("./Index");
        }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {

            var NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var  NameFilter = searchString;

            Users = (List<PhotographyUser>)await _userManager.GetUsersInRoleAsync("Photographer");

            switch (sortOrder)
            {
                case "name_desc":
                    Users = Users.OrderByDescending(s => s.UserName).ToList();
                    break;
                default:
                    Users = Users.OrderBy(s => s.Name).ToList();
                    break;
            }

            if (NameFilter != null)
                Users = Users.Where(s => s.Name.Contains(NameFilter) || s.UserName.Contains(NameFilter)).ToList();
        }
    }
}
