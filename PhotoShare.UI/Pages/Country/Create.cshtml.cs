using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoShare.Data;
using PhotoShare.Domain.Values;
using PhotoShare.Extensions;

namespace PhotoShare.Pages.Country
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string errorMessage = null)
        {
            ErrorMessage = errorMessage;
            return Page();
        }

        public string ErrorMessage { get; set; }
        [BindProperty]
        public Domain.Values.Country Country { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            Country.ID = Guid.NewGuid().ToString();
            
            if (!ModelState.IsValid)
            {
                ErrorMessage = ModelState.GetModelStateErrorMeggages();
                return RedirectToPage("./Create", new { errorMessage = ErrorMessage });
            }

            _context.Countries.Add(Country);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
