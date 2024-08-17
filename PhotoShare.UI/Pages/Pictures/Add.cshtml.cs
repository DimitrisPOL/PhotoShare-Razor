using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhotoShare.Data;
using PhotoShare.Domain.Aggregates;
using PhotoShare.Infrastructure.Services;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PhotoShare.Pages.Pictures
{
    public class AddModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<Domain.Values.Location> Locations;
        public List<SelectListItem> Options { get; set; }
        public IBlobStorageManager _blobStorageManager { get; set; }

        public AddModel(ApplicationDbContext context, IBlobStorageManager blobStorageManager)
        {
            Options = new List<SelectListItem>();
            _blobStorageManager = blobStorageManager;
            _context = context;
            Locations = _context.Locations.ToList();
            Locations.ForEach(c => Options.Add(new SelectListItem(c.Name, c.ID)));
        }


        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        public string ErrorMessage { get; set; }
        public List<string> FileNames { get; set; }
        [BindProperty]
        public string LocationId { get; set; }
        public List<PhotoBlob> PhotoBlobs { get; set; }

        public async Task<IActionResult> OnPostAsync(IFormFile[] photos)
        {

            if (photos != null && photos.Length > 0)
            {
                var location = _context.Locations.Where( l => l.ID == LocationId).FirstOrDefault();
                FileNames = new List<string>();

                if(location != null)
                    foreach (IFormFile photo in photos)
                    {
                        await _blobStorageManager.UploadBlobPicture(location.Name.ToLowerInvariant(), photo.FileName.ToLowerInvariant(), photo.OpenReadStream());
                        FileNames.Add(photo.FileName);
                    }
            }

            return RedirectToPage("../Location/Details", new { id = LocationId });
        }
     }
}
