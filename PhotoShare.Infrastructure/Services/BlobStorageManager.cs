using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using PhotoShare.Domain.Aggregates;
using PhotoShare.Infrastructure.Configuration;
using System.Globalization;
using UnidecodeSharpFork;

namespace PhotoShare.Infrastructure.Services
{
    public class BlobStorageManager : IBlobStorageManager
    {
        private readonly BlobServiceClient blobServiceClient;
        private ApplicationConfiguration Configuration;
        public BlobStorageManager()
        {

        }
        public BlobStorageManager(IOptions<ApplicationConfiguration> optionsSnapshot)
        {
            Configuration = optionsSnapshot.Value;
            try
            {
                blobServiceClient = new BlobServiceClient(Configuration.Settings.BlobStorageSettings.ConnectionString);
            }
            catch (Exception ex)
            {
                blobServiceClient = default;
            }


        }

        public async Task CreateContainer(string name)
        {
            CultureInfo en = new CultureInfo("en-US");
            name = name.ToString(en);
            BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(name.ToLowerInvariant().Unidecode());
            await containerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
            var client = blobServiceClient.GetBlobContainerClient(name.ToLowerInvariant());
            await client.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
        }

        public async Task DeletePictures(string blobName, string fileName, Stream path)
        {
        }

            public async Task UploadBlobPicture(string blobName, string fileName, Stream path)
        {
            //string containerName = "quickstartblobs";\    
            CultureInfo en = new CultureInfo("en-US");
            blobName = blobName.ToString(en);
            // Create the container and return a container client object
            var client = blobServiceClient.GetBlobContainerClient(blobName.Unidecode());
            
            // BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
            // client.ExistsAsync(name);
            //     var blobs = client.GetBlobs();

            // Open the file and upload its data
            // var fileStream = File.OpenRead(@"C:\Users\dimip\Pictures\kinites4.jpg");

            // var atr = File.GetAttributes(@"C:\Users\dimip\Pictures\kinites4.jpg");

            // blobClient.SetHttpHeaders
            try
            {
                string contentType = Path.GetExtension(fileName);
                var header = new BlobHttpHeaders();

                if (contentType == ".jpg" || contentType == ".jpeg")
                    header.ContentType = "image/jpeg";
                else if (contentType == ".png")
                    header.ContentType = "image/png";

                if(header.ContentType != null)
                {
                    BlobClient blobClient = client.GetBlobClient(fileName.Unidecode());
                    var resp = await blobClient.UploadAsync(path, header);
                }
    
            }
            catch (Exception ex)
            {
                
            }

            //fileStream.Close();
        }

        public async Task<List<PhotoBlob>> GetPictures(string name, int pageSize = 2, int skip = 0)
        {

            List<PhotoBlob> photos = new List<PhotoBlob>();
            BlobContainerClient client = default;
            try
            {
                name = name?.ToLowerInvariant();
                client = blobServiceClient.GetBlobContainerClient(name.Unidecode());
                await client.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
            }
            catch(Exception ex)
            {
                return null;
            }


            var blobs = client.GetBlobs().Skip(skip).Take(pageSize).ToList();


            //var blobs2 = (IAsyncEnumerator<BlobItem>)client.GetBlobsAsync();
            //await foreach (var blobItem in blobs2)
            //{

            //}
                //.Skip(skip).Take(2).ToList();
            // result.
            // Enumerate the blobs returned for each page.
            foreach (BlobItem blobItem in blobs)
            {
                //var result = blobPage.Values.Skip(skip).ToList();
                //foreach (BlobItem blobItem in blobPage)
                //{
                    var builder = new UriBuilder(Configuration.Settings.BlobStorageSettings.Scheme, $"{Configuration.Settings.BlobStorageSettings.AzureSotrageAccountUrl}");
                     builder.Path = $"{name}/{blobItem.Name}";
                    photos.Add(new PhotoBlob(blobItem.Name, builder.Uri));
               // }
            }

            return photos;

        }

            //public async Task<bool> CheckIfBlobExists(string blobname)
            //{
            //    blobServiceClient.
            //}


        }
}
