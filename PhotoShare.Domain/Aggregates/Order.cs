using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoShare.Data.Models
{
    public class Order
    {
        public string ID { get; set; }
        public string OrderFriendlyName { get; set; }
        public string PhotographerId { get; set; }
        public string UserId { get; set; }
        public Uri DownloadPictures { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public bool IsUserViewed { get; set; }
        public bool IsPhotoViewed { get; set; }
        public int NumberOfPics { get; set; }
        public decimal Duration { get; set; }
        public DateTime OrderedAt { get; set; }
        public DateTime ToShootAt { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
