//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

namespace MasterStream.Core.API.Brokers.Blobs
{
    public partial class BlobBroker
    {
        public async Task<string> UploadVideoAsync(Stream fileStream, string fileName, string contentType) =>
            await UploadAsync(fileStream, fileName, contentType);
    }
}
