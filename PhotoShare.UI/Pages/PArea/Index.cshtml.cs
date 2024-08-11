using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Data;
using PhotoShare.Domain.Values;

namespace PhotoShare.Pages.PArea
{
    [Authorize(Roles = "ADMIN")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Area> Area { get;set; }

        public async Task OnGetAsync()
        {
            Area = await _context.Areas.ToListAsync();
        }
    }
}
