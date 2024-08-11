using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhotoShare.Data;
using PhotoShare.Domain.Aggregates;
using PhotoShare.Domain.Values;
using PhotoShare.Infrastructure.Services;
using UnidecodeSharpFork;


namespace PhotoShare.Pages.Loc
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Area> Areas;
        public List<SelectListItem> Cities { get; set; }
        public IBlobStorageManager _blobStorageManager { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }

        public CreateModel(ApplicationDbContext context, IBlobStorageManager blobStorageManager)
        {
            
            Cities = new List<SelectListItem>();
            _blobStorageManager = blobStorageManager;
            _context = context;
            Areas = _context.Areas.ToList();
            Areas.ForEach(c => Cities.Add(new SelectListItem(c.Name, c.ID)));
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Location Location { get; set; }
        [BindProperty]
        public string AreaId { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile upload)
        {

            if (upload != null && upload.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    upload.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    Location.ProfilePic = ms.ToArray();
                    // string s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                }
            }

            Location.Area = Areas.Where(c => c.ID == AreaId).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Locations.Add(Location);
            await _context.SaveChangesAsync();
            var text = Location.ID.Unidecode();
            await _blobStorageManager.CreateContainer(Location.ID.Unidecode());

            return RedirectToPage("./Index");
        }
    }
}
