//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Models.Photos;

namespace MasterStream.Core.API.Services.Photos
{
    public interface IPhotoService
    {
        Task<string> AddPhotoAsync(Stream fileStream, string fileName, string contentType);
    }
}
