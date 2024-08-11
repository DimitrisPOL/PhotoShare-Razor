using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PhotoShare.Domain.Aggregates;
using PhotoShare.Infrastructure.Services;

namespace PhotoShare.Pages.Loc
{
    [DisableRequestSizeLimit]
    [Authorize(Roles = "admin")]
    public class DetailsModel : PageModel
    {
        private readonly PhotoShare.Data.ApplicationDbContext _context;
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _ihostingEnvironment;
        private IBlobStorageManager _blobStorageManager;

        public DetailsModel(PhotoShare.Data.ApplicationDbContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment ihostingEnvironment, IBlobStorageManager blobStorageManager)
        {
            _context = context;
            _ihostingEnvironment = ihostingEnvironment;
            _blobStorageManager = blobStorageManager;
        }

        public Location Location { get; set; }
        public List<PhotoBlob> PhotoBlobs { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {

            if (id == null)
            {
                return NotFound();
            }

            Location = await _context.Locations.FirstOrDefaultAsync(m => m.ID == id);

            if (Location == null)
            {
                return NotFound();
            }

            PhotoBlobs = await _blobStorageManager.GetPictures(Location.ID, 40);

            return Page();
        }

        public List<string> FileNames { get; set; }
        [BindProperty]
        public string Name { get; set; }

  
        public async Task<IActionResult> OnPostAsync(string id, IFormFile[] photos)
        {
            if (photos != null && photos.Length > 0)
            {
                FileNames = new List<string>();
                foreach (IFormFile photo in photos)
                {
                    string test = Location?.Name;
                  
                    var path = Path.Combine(_ihostingEnvironment.WebRootPath, "images", photo.FileName);


                    //Uri uri = new Uri(hreflink);
                    //if (uri.IsFile)
                    //{
                    //    string filename = System.IO.Path.GetFileName(uri.LocalPath);
                    //}
                   
                    //var stream = new FileStream(path, FileMode.Create);
                    await _blobStorageManager.UploadBlobPicture(Name.ToLowerInvariant(), photo.FileName.ToLowerInvariant(), photo.OpenReadStream());
                   // await photo.CopyToAsync(stream);
                    FileNames.Add(photo.FileName);
                }
            }

            return await OnGetAsync(id);

        }
    }
}
