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
            blobServiceClient = new BlobServiceClient(Configuration.Settings.BlobStorageSettings.ConnectionString);
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

        public async Task DeletePictures(string blobName)
        {
            BlobContainerClient client = blobServiceClient.GetBlobContainerClient(blobName.ToLowerInvariant());
            if (client == null)
                return;
            else
                await client.DeleteAsync();
        }

        public async Task UploadBlobPicture(string blobName, string fileName, Stream path)
        {
            CultureInfo en = new CultureInfo("en-US");
            blobName = blobName.ToString(en);
            BlobContainerClient client = default;

            client = blobServiceClient.GetBlobContainerClient(blobName.Unidecode());

            if (!client.Exists())
                await blobServiceClient.CreateBlobContainerAsync(blobName.Unidecode());
            try
            {
                string contentType = Path.GetExtension(fileName);
                var header = new BlobHttpHeaders();

                if (contentType == ".jpg" || contentType == ".jpeg")
                    header.ContentType = "application/octet-stream";
                else if (contentType == ".png")
                    header.ContentType = "application/octet-stream";

                if (header.ContentType != null)
                {
                    BlobClient blobClient = client.GetBlobClient(fileName.Unidecode());
                    var resp = await blobClient.UploadAsync(path, header);
                }

            }
            catch (Exception ex)
            {

            }
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
            catch (Exception ex)
            {
                return null;
            }


            var blobs = client.GetBlobs().Skip(skip).Take(pageSize).ToList();

            foreach (BlobItem blobItem in blobs)
            {
                var builder = new UriBuilder(Configuration.Settings.BlobStorageSettings.Scheme, $"{Configuration.Settings.BlobStorageSettings.AzureSotrageAccountUrl}");
                builder.Path = $"{name}/{blobItem.Name}";
                photos.Add(new PhotoBlob(blobItem.Name, builder.Uri));
            }

            return photos;

        }
    }
}
