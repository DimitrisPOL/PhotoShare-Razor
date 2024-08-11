using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoShare.Domain.Aggregates
{
    public class PhotoBlob
    {
        public PhotoBlob(string name, Uri url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; set; }
        public Uri Url { get; set; }
    }
}
