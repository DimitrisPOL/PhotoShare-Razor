using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PhotoShare.Pages.Pictures
{
    public class AddModel : PageModel
    {
        private IHostingEnvironment ihostingEnvironment;

        public AddModel(IHostingEnvironment ihostingEnvironment)
        {
            this.ihostingEnvironment = ihostingEnvironment;
        }


        public void OnGet()
        {
        }

        public string ErrorMessage { get; set; }
        public List<string> FileNames { get; set; }

        public async Task<IActionResult> OnPostAsync(IFormFile[] photos)
        {
            if (photos != null && photos.Length > 0)
            {
                FileNames = new List<string>();
                foreach (IFormFile photo in photos)
                {
                    var path = Path.Combine(ihostingEnvironment.WebRootPath, "images", photo.FileName);
                    var stream = new FileStream(path, FileMode.Create);
                    await photo.CopyToAsync(stream);
                    FileNames.Add(photo.FileName);
                }
            }

            return RedirectToPage("./Index");
        }
     }
}
