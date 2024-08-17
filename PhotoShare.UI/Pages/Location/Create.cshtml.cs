using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhotoShare.Data;
using PhotoShare.Domain.Values;
using PhotoShare.Extensions;
using PhotoShare.Infrastructure.Services;
using UnidecodeSharpFork;


namespace PhotoShare.Pages.Location
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Domain.Values.Area> Areas;
        public List<SelectListItem> Options { get; set; }
        public IBlobStorageManager _blobStorageManager { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }

        public CreateModel(ApplicationDbContext context, IBlobStorageManager blobStorageManager)
        {

            Options = new List<SelectListItem>();
            _blobStorageManager = blobStorageManager;
            _context = context;
            Areas = _context.Areas.ToList();
            Areas.ForEach(c => Options.Add(new SelectListItem(c.Name, c.ID)));
        }

        public IActionResult OnGet(string errorMessage = null)
        {
            ErrorMessage = errorMessage;
            return Page();
        }

        public string ErrorMessage { get; set; }
        [BindProperty]
        public Domain.Values.Location Location { get; set; }
        [BindProperty]
        public string AreaId { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(IFormFile upload)
        {
            Location.ID = Guid.NewGuid().ToString();

            if (!ModelState.IsValid)
            {
                ErrorMessage = ModelState.GetModelStateErrorMeggages();
                return RedirectToPage("./Create", new { errorMessage = ErrorMessage });
            }

            if (upload != null && upload.Length > 0)
                using (var ms = new MemoryStream())
                {
                    upload.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    Location.ProfilePic = ms.ToArray();
                }

            var area = Areas.Where(c => c.ID == AreaId).FirstOrDefault();
            if (area != null)
                Location.Area = area;
            else
                return RedirectToPage("./Create", new { errorMessage ="Area is required" });

            Location.ID = Guid.NewGuid().ToString();

            _context.Locations.Add(Location);
            await _context.SaveChangesAsync();
            await _blobStorageManager.CreateContainer(Location.ID.Unidecode());

            return RedirectToPage("./Index");
        }
    }
}
