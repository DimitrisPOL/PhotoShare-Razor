using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PhotoShare.Data;
using PhotoShare.Infrastructure.Configuration;
using PhotoShare.Infrastructure.Services;
using PhotoShare.Domain.Aggregates;

namespace PhotoShare.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IBlobStorageManager _blobStorageManager;
        public IList<Domain.Values.Location> Location { get; set; }
        private readonly ApplicationDbContext _context;
        private ApplicationConfiguration Configuration;
        public List<PhotoBlob> PhotoBlobs { get; set; }
        public string NameSort { get; set; }
        public string NameFilter { get; set; }
        public string Url { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IBlobStorageManager blobStorageManager, ApplicationDbContext context, IOptions<ApplicationConfiguration> optionsSnapshot)
        {
            Configuration = optionsSnapshot.Value;
            _blobStorageManager = blobStorageManager;
            _context = context;
            _logger = logger;

        }

        public async Task OnGetAsync(string searchString, int indexCount = 0)
        {
            try
            {

                NameFilter = searchString;
                //Location = new List<Location>();
                List<string> LocationToDisplay = Configuration.Settings.LocationsSettings.LocationsToDisplay.Select(l => l.Value).ToList();

                Url = Configuration.Settings.BaseUrl;


                if (LocationToDisplay.Count == 1)
                {
                    Location = await _context.Locations.ToListAsync();
                }
                else
                    Location = await _context.Locations.Where(l => LocationToDisplay.Contains(l.Name)).ToListAsync();



                if (searchString == null)
                {

                    PhotoBlobs = new List<PhotoBlob>();
                    foreach (var loc in Location)
                    {
                        var blobs = await _blobStorageManager.GetPictures(loc.Name, Configuration.Settings.LocationsSettings.PicsPerLocation, 0);
                        if (blobs != null)
                            PhotoBlobs.AddRange(blobs);
                    }
                }
                else
                {
                    var loc = _context.Locations.Where(l => l.Name.Contains(searchString) || l.SearchIndex.Contains(searchString)).FirstOrDefault();
                    PhotoBlobs = new List<PhotoBlob>();

                    if (loc != null)
                    {
                        var blobs = await _blobStorageManager.GetPictures(loc?.Name, Configuration.Settings.LocationsSettings.PicsPerLocation * 5, 0);
                        if (blobs != null)
                            PhotoBlobs.AddRange(blobs);
                    }



                }

            }
            catch (Exception ex) { }

        }

        public async Task<IActionResult> OnGetNewPictures(string searchString, int skip)
        {
            Location = new List<Domain.Values.Location>();
            Location = await _context.Locations.ToListAsync();

            if (searchString == null)
            {

                List<string> PhotoBlobs = new List<string>();
                int count = Location.Count();
                foreach (var loc in Location)
                {
                    var blobs = await _blobStorageManager.GetPictures(loc.Name, Configuration.Settings.LocationsSettings.PicsPerLocation, skip * Configuration.Settings.LocationsSettings.PicsPerLocation);
                    if (blobs != null)
                        PhotoBlobs.AddRange(blobs.Select(b => b.Url.ToString()).ToList());
                }
                return new JsonResult(PhotoBlobs.ToArray());

            }
            else
            {
                var loc = _context.Locations.Where(l => l.Name.Contains(searchString) || l.SearchIndex.Contains(searchString)).FirstOrDefault();
                PhotoBlobs = new List<PhotoBlob>();

                if (loc == null)
                    return null;

                var blobs = await _blobStorageManager.GetPictures(loc.Name, Configuration.Settings.LocationsSettings.PicsPerLocation * 5, skip * Configuration.Settings.LocationsSettings.PicsPerLocation * 5);
                return new JsonResult(blobs.Select(b => b.Url.ToString()).ToArray());

                return null;

            }

            return null;
        }

        //public IActionResult OnPost()
        //{


        //    return new JsonResult("aaa");
        //}
        public IActionResult OnGetAutoComplete(string prefix)
        {
            var locations = _context.Locations.Where(l => l.Name.Contains(prefix) || l.SearchIndex.Contains(prefix)).Select(l => l.Name).ToList();

            //(from location in this._context.Locations
            //             where location.Name.ToLowerInvariant().Contains(prefix.ToLowerInvariant())
            //             select new
            //             {
            //                 label = location.Name,
            //                 val = location.ID
            //             }).ToList();

            return new JsonResult(locations);
        }
    }
}
