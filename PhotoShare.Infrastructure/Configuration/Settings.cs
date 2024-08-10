using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoShare.Infrastructure.Configuration
{
    public class Settings
    {
        public BlobStorageSettings BlobStorageSettings { get; set; }
        public LocationsSettings LocationsSettings { get; set; }
        public string BaseUrl { get; set; }
        public EmailSettings EmailSettings { get; set; }

    }
}
