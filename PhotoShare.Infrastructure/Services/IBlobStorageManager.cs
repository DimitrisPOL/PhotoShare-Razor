using PhotoShare.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoShare.Infrastructure.Services
{
    public interface IBlobStorageManager
    {
        public Task CreateContainer(string name);
        public Task UploadBlobPicture(string blobName, string fileName, Stream path);
        public Task<List<PhotoBlob>> GetPictures(string name, int pageSize = 2, int skip = 0);
    }
}
