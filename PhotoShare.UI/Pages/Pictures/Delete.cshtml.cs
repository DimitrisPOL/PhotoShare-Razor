using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Domain.Aggregates;
using PhotoShare.Domain.Values;
using PhotoShare.Infrastructure.Data.Users;
using PhotoShare.Infrastructure.Services;


namespace PhotoShare.Pages.Pictures
{
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
    {
        public List<PhotographyUser> Users;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<PhotographyUser> _userManager;
        public IBlobStorageManager _blobStorageManager { get; set; }

        public DeleteModel(ApplicationDbContext context, 
            UserManager<PhotographyUser> userManager, IBlobStorageManager blobStorageManager)
        {
            _context = context;
            _userManager = userManager;
            _blobStorageManager = blobStorageManager;
        }


        public async Task<ActionResult> OnGet(string id = null)
        {
            if (id != null)
            {
                 await _blobStorageManager.DeletePictures(id);
            }

            return RedirectToPage("./Index");
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
