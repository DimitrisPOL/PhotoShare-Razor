using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoShare.Domain.Values
{
    public class Location
    {
        public string? ID { get; set; }
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [RegularExpression("^[a-zA-Z]{3,20}$", ErrorMessage = "Name must only contain latin characters")]
        public string Name { get; set; }
        public string SearchIndex { get; set; }
        public byte[]? ProfilePic { get; set; }
        public Area? Area { get; set; }
        public string TelephonePrefix1 { get; set; }
        public int TelephonePrefix2 { get; set; }
        public int NumberOfOutposts { get; set; }
        public long NumberOfPictures { get; set; }
        public long NumberOfPhotographers { get; set; }
    }
}
