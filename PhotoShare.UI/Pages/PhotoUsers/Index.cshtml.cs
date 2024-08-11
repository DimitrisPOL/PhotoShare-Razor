using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Domain.Values;


namespace PhotoShare.Pages.PhotoUsers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Photographer> Photographer { get;set; }

        public async Task OnGetAsync()
        {
            Photographer = await _context.Photographers.ToListAsync();
        }
    }
}
