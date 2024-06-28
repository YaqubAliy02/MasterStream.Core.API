//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using MasterStream.Core.API.Models.Videos;

namespace MasterStream.Core.API.Brokers.Blobs
{
    public partial class BlobBroker
    {
        public async Task<string> UploadVideoAsync(Stream fileStream, string fileName, string contentType) =>
            await UploadAsync(fileStream, fileName, contentType);
    }
}
