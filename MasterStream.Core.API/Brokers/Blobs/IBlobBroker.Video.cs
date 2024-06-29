//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Models.Videos;

namespace MasterStream.Core.API.Brokers.Blobs
{
    public partial interface IBlobBroker
    {
        Task<string> UploadVideoAsync(Stream fileStream, string fileName, string contentType);
    }
}
