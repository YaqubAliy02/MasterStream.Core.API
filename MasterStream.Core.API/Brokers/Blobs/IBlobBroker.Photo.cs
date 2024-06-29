//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Models.Photos;
using MasterStream.Core.API.Models.Videos;

namespace MasterStream.Core.API.Brokers.Blobs
{
    public partial interface IBlobBroker
    {
        Task<string> UploadPhotoAsync(Stream fileStream, string fileName, string contentType);
    }
}
