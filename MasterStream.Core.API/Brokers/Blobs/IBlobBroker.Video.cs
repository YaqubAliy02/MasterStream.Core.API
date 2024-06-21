//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

namespace MasterStream.Core.API.Brokers.Blobs
{
    public partial interface IBlobBroker
    {
        Task<string> UploadVideoAsync(Stream fileStream, string fileName, string contentType);
    }
}
