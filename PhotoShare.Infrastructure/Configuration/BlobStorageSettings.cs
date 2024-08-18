using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoShare.Infrastructure.Configuration
{
    public class BlobStorageSettings
    {
        public string Scheme { get; set; }
        public string AzureSotrageAccountUrl { get; set; }
        public string ConnectionString { get; set; }
    }
}
