using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using PhotoShare.Infrastructure.Configuration;

namespace PhotoShare.Pages.Pictures
{
    public class PhotoDetailsModel : PageModel
    {
        private ApplicationConfiguration Configuration;
        public string Pic;
        public string Url;
        public string Name;

        public PhotoDetailsModel(IOptions<ApplicationConfiguration> optionsSnapshot)
        {
            Configuration = optionsSnapshot.Value;
        }

        public void OnGet(string photoUrl, string name)
        {
            Pic = HttpUtility.UrlDecode(photoUrl);
            Name = name;
            Url = $"{Configuration.Settings.BaseUrl}/PhotoDetails/{photoUrl}";
        }
    }
}
