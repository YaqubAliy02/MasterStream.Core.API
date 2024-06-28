//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

namespace MasterStream.Core.API.Brokers.Blobs
{
    public partial interface IBlobBroker
    {
        Task<Stream> GetBlobStreamAsync(string blobName, string containerName);
    }
}
