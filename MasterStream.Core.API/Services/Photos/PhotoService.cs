//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Brokers.Blobs;
using MasterStream.Core.API.Brokers.DateTimes;
using MasterStream.Core.API.Brokers.Loggings;

namespace MasterStream.Core.API.Services.Photos
{
    public class PhotoService : IPhotoService
    {
        private readonly IBlobBroker blobBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public PhotoService(
            IBlobBroker blobBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.blobBroker = blobBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public async Task<string> AddPhotoAsync(Stream fileStream, string fileName, string contentType) =>
            await this.blobBroker.UploadPhotoAsync(fileStream, fileName, contentType);
    }
}
