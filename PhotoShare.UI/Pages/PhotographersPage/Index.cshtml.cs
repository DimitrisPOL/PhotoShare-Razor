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


namespace PhotoShare.Pages.PhotographersPage
{
    [Authorize(Roles = "ADMIN")]
    public class IndexModel : PageModel
    {
        public List<PhotographyUser> Users;
        private readonly UserManager<PhotographyUser> _userManager;
        public RoleManager<IdentityRole> _rolemanager;
        public string NameSort { get; set; }
        public string NameFilter { get; set; }
        public IndexModel(UserManager<PhotographyUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _rolemanager = roleManager;
            Users = new List<PhotographyUser>();
        }

        public async Task OnGetManage(string command, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (command == "enable")
            {   
                if(user.LockoutEnabled)
                {
                    await _userManager.SetLockoutEnabledAsync(user, false);
                    user = await _userManager.FindByIdAsync(id);
                }

                if(!user.DisplayToFront)
                {
                    user.DisplayToFront = true;
                    await _userManager.UpdateAsync(user);
                }
                  
        
            }
            else if(command == "disable")
            {
                await _userManager.SetLockoutEnabledAsync(user, true);
                user.DisplayToFront = false;
                await _userManager.UpdateAsync(user);
            }
            else if(command == "confirm")
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.ConfirmEmailAsync(user, code);

            }


        }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            bool x = await _rolemanager.RoleExistsAsync("admin");
            if (!x)
            {
                // first we create admin rool    
                var role = new IdentityRole();
                role.Name = "admin";
                await _rolemanager.CreateAsync(role);
            }
            x = await _rolemanager.RoleExistsAsync("photographer");
            if (!x)
            {
                // first we create admin rool    
                var role = new IdentityRole();
                role.Name = "photographer";
                await _rolemanager.CreateAsync(role);
            }

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            NameFilter = searchString;

            Users = (List<PhotographyUser>)await _userManager.GetUsersInRoleAsync("Photographer");

            //_userManager.Confir

            switch (sortOrder)
            {
                case "name_desc":
                    Users = Users.OrderByDescending(s => s.UserName).ToList();
                    break;
                //case "Date":
                //    UsersIQ = UsersIQ.OrderBy(s => s.);
                //    break;
                //case "date_desc":
                //    UsersIQ = UsersIQ.OrderByDescending(s => s.EnrollmentDate);
                //    break;
                default:
                    Users = Users.OrderBy(s => s.Name).ToList();
                    break;
            }

            if (NameFilter != null)
                Users = Users.Where(s => s.Name.Contains(NameFilter) || s.UserName.Contains(NameFilter)).ToList();

        }
    }
}
