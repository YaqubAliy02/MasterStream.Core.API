//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

namespace MasterStream.Core.API.Brokers.Blobs
{
    public partial interface IBlobBroker
    {
        Task<IEnumerable<string>> GetAllVideoFilesAsync();
        //Task<Stream> GetStreamAsync(string fileName, string containerName);
        Task<Stream> GetBlobStreamAsync(string blobName, string containerName);
    }
}
