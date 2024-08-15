using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhotoShare.Data;
using PhotoShare.Domain.Values;
using PhotoShare.Extensions;


namespace PhotoShare.Pages.Province
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<SelectListItem> Countries { get; set; }


    public CreateModel(ApplicationDbContext context)
        {
            Countries = new List<SelectListItem>();
            _context = context;
            _context.Countries.ToList().ForEach( c => Countries.Add(new SelectListItem( c.Name, c.ID)));
        }

        public IActionResult OnGet(string errorMessage = null)
        {
            ErrorMessage = errorMessage;
            return Page();
        }

        public string ErrorMessage { get; set; }

        [BindProperty]
        public Domain.Values.Province Province { get; set; }
        [BindProperty]
        public string CountryId { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = ModelState.GetModelStateErrorMeggages();
                return RedirectToPage("./Create", new { errorMessage = ErrorMessage });
            }

            var country = _context.Countries.Where(c => c.ID == CountryId).FirstOrDefault();
            if(country != null)
                Province.Country = country;
            else
                return RedirectToPage("./Create", new { errorMessage = "You must select country" });


            Province.ID = Guid.NewGuid().ToString();

            _context.Provinces.Add(Province);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
