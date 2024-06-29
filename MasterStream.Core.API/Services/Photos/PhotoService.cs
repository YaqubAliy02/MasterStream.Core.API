//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Brokers.Blobs;

namespace MasterStream.Core.API.Services.Photos
{
    public class PhotoService : IPhotoService
    {
        private readonly IBlobBroker blobBroker;

        public PhotoService(IBlobBroker blobBroker)
        {
            this.blobBroker = blobBroker;
        }

        public async Task<string> AddPhotoAsync(Stream fileStream, string fileName, string contentType) =>
            await this.blobBroker.UploadPhotoAsync(fileStream, fileName, contentType);
    }
}
