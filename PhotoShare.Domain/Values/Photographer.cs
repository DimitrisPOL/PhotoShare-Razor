using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoShare.Domain.Values
{
    public class Photographer
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public  DateTime DOB { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public byte[] ProfilePic { get; set; }
        public string Degree { get; set; }
        public int YearsOfExperience { get; set; }
        public string PhoneNumber { get; set; }
    }
}
