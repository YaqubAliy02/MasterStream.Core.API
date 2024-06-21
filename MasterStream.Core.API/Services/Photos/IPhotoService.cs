//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

namespace MasterStream.Core.API.Services.Photos
{
    public interface IPhotoService
    {
        Task<string> AddPhotoAsync(Stream fileStream, string fileName, string contentType);
    }
}
