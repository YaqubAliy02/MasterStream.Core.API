//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Brokers.Blobs;
using MasterStream.Core.API.Brokers.DateTimes;
using MasterStream.Core.API.Brokers.Loggings;
using MasterStream.Core.API.Models.VideoMetadatas.Brokers.Storages;

namespace MasterStream.Core.API.Services.Photos
{
    public class PhotoService : IPhotoService
    {
        private readonly IBlobBroker blobBroker;
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public PhotoService(
            IBlobBroker blobBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker,
            IStorageBroker storageBroker)
        {
            this.blobBroker = blobBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
            this.storageBroker = storageBroker;
        }

        public async Task<string> AddPhotoAsync(Stream fileStream, string fileName, string contentType) =>
            await this.blobBroker.UploadPhotoAsync(fileStream, fileName, contentType);
    }
}
