//--------------------------
// TARTEEB LLC
// ALL RIGHTS RESERVED
//--------------------------

using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
namespace MasterStream.Core.Web.Services
{
    public class BlobService
    {
        private readonly string connectionString;

        public BlobService(IConfiguration configuration)
        {
            this.connectionString = configuration.GetSection("AzureBlobStorage: ConnectionString").Value;
        }

        public async Task<List<string>> ListBlobsAsync(string containerName)
        {
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            var blobUrls = new List<string>();

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                var blobClient = containerClient.GetBlobClient(blobItem.Name);
                blobUrls.Add(blobClient.Uri.ToString());
            }
            return blobUrls;
        }


        /*   public BlobService()
           {
               var credential  = new StorageSharedKeyCredential(storageAccount, accountKey);
               var blobUrl = $"https://{storageAccount}.blob.core.windows.net";
               var client = new BlobServiceClient(new Uri(blobUrl), credential);
               this.photoContainer = client.GetBlobContainerClient("photos");
           }*/


        /*  public async Task<List<Photo>> GetPhotos()
          {
                  var photos = new List<Photo>();
              var photoBlobs = this.photoContainer.GetBlobsAsync();

              await foreach (var photo in photoBlobs)
              {
                  var blob = this.photoContainer.GetBlobClient(photo.Name);
                  var url = blob.Uri.ToString();
                  photos.Add(new Photo { FileName = photo.Name, BlobUri = url });
              }

              return photos;
          }*/
    }
}
