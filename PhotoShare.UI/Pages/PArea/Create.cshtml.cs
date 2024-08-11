using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhotoShare.Data;
using PhotoShare.Domain.Values;

namespace PhotoShare.Pages.PArea
{
    [Authorize(Roles = "ADMIN")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Province> provinces;
        public List<SelectListItem> Cities { get; set; }

        public CreateModel(ApplicationDbContext context)
        {
            Cities = new List<SelectListItem>();
            _context = context;
            provinces = _context.Provinces.ToList();
            provinces.ForEach(c => Cities.Add(new SelectListItem(c.Name, c.ID)));
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Area Area { get; set; }

        [BindProperty]
        public string ProvinceID { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Area.Country = provinces.Where(c => c.ID == ProvinceID).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Areas.Add(Area);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
