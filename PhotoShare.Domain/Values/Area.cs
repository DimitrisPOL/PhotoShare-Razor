using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoShare.Domain.Values
{
    public class Area
    {
        public string? ID { get; set; }
        public string Name { get; set; }
        public Province? Province { get; set; }
        public string TelephonePrefix1 { get; set; }
        public int TelephonePrefix2 { get; set; }
        public int NumberOfOutposts { get; set; }
        public long NumberOfPictures { get; set; }
        public long NumberOfPhotographers { get; set; }
    }
}
