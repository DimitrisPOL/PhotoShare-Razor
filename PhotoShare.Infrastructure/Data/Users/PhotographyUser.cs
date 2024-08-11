using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoShare.Infrastructure.Data.Users
{
    public class PhotographyUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public string City { get; set; }
        [PersonalData]
        public byte[] ProfilePic { get; set; }
        [PersonalData]
        public string Degree { get; set; }
        [PersonalData]
        public int YearsOfExperience { get; set; }
        [PersonalData]
        public bool DisplayToFront { get; set; }

        [PersonalData]
        public DateTime DOB
        {
            get; set;
        }
    }
}
