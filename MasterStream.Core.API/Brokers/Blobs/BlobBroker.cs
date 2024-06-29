//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MasterStream.Core.API.Models.Videos;

namespace MasterStream.Core.API.Brokers.Blobs
{
    public partial class BlobBroker : IBlobBroker
    {
        private readonly string blobConnectionString;
        private readonly string videoContainerName;
        private readonly string photoContainerName;
        private readonly HashSet<string> videoExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".mp4", ".avi", ".mov" };
        private readonly HashSet<string> photoExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jpg", ".jpeg", ".png", ".gif" };

        public BlobBroker(IConfiguration configuration)
        {
            blobConnectionString = configuration["AzureBlobStorage:ConnectionString"];
            videoContainerName = configuration["AzureBlobStorage:VideoContainerName"];
            photoContainerName = configuration["AzureBlobStorage:PhotoContainerName"];
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            var blobContainerName = GetContainerName(extension);

            if (blobContainerName == null)
            {
                throw new InvalidOperationException("Unsupported file type.");
            }

            var blobServiceClient = new BlobServiceClient(blobConnectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(fileStream, true);

            return blobClient.Uri.ToString();
        }

        public async Task<Stream> GetBlobStreamAsync(string blobName, string containerName)
        {
            var blobServiceClient = new BlobServiceClient(blobConnectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();

            var memoryStream = new MemoryStream();
            await blobDownloadInfo.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            return memoryStream;
        }

        private string GetContainerName(string extension)
        {
            if (videoExtensions.Contains(extension))
            {
                return videoContainerName;
            }
            if (photoExtensions.Contains(extension))
            {
                return photoContainerName;
            }
            return null;
        }
    }
}
