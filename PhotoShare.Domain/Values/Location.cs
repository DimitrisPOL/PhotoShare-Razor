using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoShare.Domain.Values
{
    public class Location
    {
        [RegularExpression("^[a-zA-Z0-9-]{3,20}$")]
        public string ID { get; set; }
        public string Name { get; set; }
        public string SearchIndex { get; set; }
        public byte[] ProfilePic { get; set; }
        public Area Area { get; set; }
        public string TelephonePrefix1 { get; set; }
        public int TelephonePrefix2 { get; set; }
        public int NumberOfOutposts { get; set; }
        public long NumberOfPictures { get; set; }
        public long NumberOfPhotographers { get; set; }
    }
}
