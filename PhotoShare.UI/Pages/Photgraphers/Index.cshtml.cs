using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Infrastructure.Data.Users;

namespace PhotoShare.Areas.Identity.Pages.Photgraphers
{
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
