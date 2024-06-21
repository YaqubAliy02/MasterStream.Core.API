//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Brokers.Blobs;
using MasterStream.Core.API.Brokers.DateTimes;
using MasterStream.Core.API.Brokers.Loggings;

namespace MasterStream.Core.API.Services.Videos
{
    public partial class VideoService : IVideoService
    {
        private readonly IBlobBroker blobBroker;
        private readonly ILoggingBroker loggingBroker;
        private readonly IDateTimeBroker dateTimeBroker;

        public VideoService(
            IBlobBroker blobBroker, 
            ILoggingBroker loggingBroker, 
            IDateTimeBroker dateTimeBroker)
        {
            this.blobBroker = blobBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimeBroker = dateTimeBroker;
        }

        public async Task<string> AddVideoAsync(Stream fileStream, string fileName, string contentType) =>
            await this.blobBroker.UploadVideoAsync(fileStream, fileName, contentType);
    }
}
