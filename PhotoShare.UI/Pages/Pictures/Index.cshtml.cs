using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhotoShare.Data;
using PhotoShare.Domain.Aggregates;
using PhotoShare.Extensions;
using PhotoShare.Infrastructure.Services;

namespace PhotoShare.Pages.Pictures
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Domain.Values.Location> Locations;
        public List<SelectListItem> Options { get; set; }
        public List<PhotoBlob> PhotoBlobs { get; set; }
        public IBlobStorageManager _blobStorageManager { get; set; }
        [BindProperty]
        public string LocationId { get; set; }

        public IndexModel(ApplicationDbContext context, IBlobStorageManager blobStorageManager)
        {
            Options = new List<SelectListItem>();
            _blobStorageManager = blobStorageManager;
            _context = context;
            Locations = _context.Locations.ToList();
            Locations.ForEach(c => Options.Add(new SelectListItem(c.Name, c.Name)));
        }
        public async Task OnGet(string id = null)
        {
            if (id != null)
            {
                LocationId = id;
                PhotoBlobs = await _blobStorageManager.GetPictures(id, 40);
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            
            return RedirectToPage($"./Index", new { id = LocationId });
        }
    }
}
