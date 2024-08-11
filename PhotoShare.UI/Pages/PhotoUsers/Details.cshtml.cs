using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Domain.Values;


namespace PhotoShare.Pages.PhotoUsers
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Photographer Photographer { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Photographer = await _context.Photographers.FirstOrDefaultAsync(m => m.ID == id);

            if (Photographer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
