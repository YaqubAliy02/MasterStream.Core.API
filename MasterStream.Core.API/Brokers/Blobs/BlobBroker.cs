//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Azure.Storage.Blobs;

namespace MasterStream.Core.API.Brokers.Blobs
{
    public class BlobBroker : IBlobBroker
    {
        private readonly string blobConnectionString;
        private readonly string blobContainerName;

        public BlobBroker(IConfiguration configuration)
        {
            blobConnectionString = configuration["AzureBlobStorage:ConnectionString"];
            blobContainerName = configuration["AzureBlobStorage:ContainerName"];
        }

        public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            var blobServiceClient = new BlobServiceClient(blobConnectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(fileStream, true);

            return blobClient.Uri.ToString();
        }
    }
}
