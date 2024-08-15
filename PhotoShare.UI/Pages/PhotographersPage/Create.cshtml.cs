using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhotoShare.Data;
using PhotoShare.Domain.Values;
using PhotoShare.Extensions;

namespace PhotoShare.Pages.PhotographersPage
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Domain.Values.Province> provinces;
        public List<SelectListItem> Cities { get; set; }

        public CreateModel(ApplicationDbContext context)
        {
            Cities = new List<SelectListItem>();
            _context = context;
            provinces = _context.Provinces.ToList();
            provinces.ForEach(c => Cities.Add(new SelectListItem(c.Name, c.ID)));
        }

        public IActionResult OnGet(string errorMessage = null)
        {
            ErrorMessage = errorMessage;
            return Page();
        }

        public string ErrorMessage { get; set; }

        [BindProperty]
        public Domain.Values.Area Area { get; set; }

        [BindProperty]
        public string ProvinceID { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = ModelState.GetModelStateErrorMeggages();
                return RedirectToPage("./Create", new { errorMessage = ErrorMessage });
            }

            var province = provinces.Where(c => c.ID == ProvinceID).FirstOrDefault();

            if (province != null)
                Area.Province = province;
            else
                return BadRequest();

            if (!ModelState.IsValid)
                return Page();

            _context.Areas.Add(Area);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
