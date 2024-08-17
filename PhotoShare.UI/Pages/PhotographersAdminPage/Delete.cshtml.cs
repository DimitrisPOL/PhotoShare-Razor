using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Domain.Values;
using PhotoShare.Infrastructure.Data.Users;


namespace PhotoShare.Pages.PhotographersAdminPage
{
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
    {
        public List<PhotographyUser> Users;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<PhotographyUser> _userManager;

        public DeleteModel(ApplicationDbContext context, 
            UserManager<PhotographyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task OnGetAsync(string sortOrder, string searchString)
        {

            var NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var NameFilter = searchString;

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


        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            await _userManager.DeleteAsync(user);

            return RedirectToPage("./Index");
        }
    }
}
