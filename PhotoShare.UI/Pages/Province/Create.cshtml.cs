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


namespace PhotoShare.Pages.Province
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Domain.Values.Country> countries;
        public List<SelectListItem> Cities { get; set; }


    public CreateModel(ApplicationDbContext context)
        {
            Cities = new List<SelectListItem>();
            _context = context;
            countries = _context.Countries.ToList();
            countries.ForEach( c => Cities.Add(new SelectListItem( c.Name, c.ID)));
        }

        public void OnGet()
        {

           // return Page();
        }

        [BindProperty]
        public Domain.Values.Province Province { get; set; }
        [BindProperty]
        public string CountryId { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var country = countries.Where(c => c.ID == CountryId).FirstOrDefault();
            if(country != null)
                Province.Country = country;
            else
                return BadRequest();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Province.ID = Guid.NewGuid().ToString();

            _context.Provinces.Add(Province);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
