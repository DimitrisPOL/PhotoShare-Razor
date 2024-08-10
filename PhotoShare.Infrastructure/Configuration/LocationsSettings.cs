using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoShare.Infrastructure.Configuration
{
    public class LocationsSettings
    {
        public Dictionary<string, string> LocationsToDisplay { get; set; }
        public int PicsPerLocation { get; set; }
        public int MaxPics{ get; set; }
    }
}
