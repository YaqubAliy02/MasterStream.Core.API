//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Brokers.Blobs;
using MasterStream.Core.API.Brokers.DateTimes;
using MasterStream.Core.API.Brokers.Loggings;
using MasterStream.Core.API.Models.VideoMetadatas.Brokers.Storages;

namespace MasterStream.Core.API.Services.Videos
{
    public partial class VideoService : IVideoService
    {
        private readonly IBlobBroker blobBroker;
        private readonly IStorageBroker storageBroker;

        public VideoService(
            IBlobBroker blobBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker,
            IStorageBroker storageBroker)
        {
            this.blobBroker = blobBroker;
            this.storageBroker = storageBroker;
        }

        public async Task<string> AddVideoAsync(Stream fileStream, string fileName, string contentType) =>
            await this.blobBroker.UploadVideoAsync(fileStream, fileName, contentType);

        public async Task<Stream> GetVideoStreamByIdAsync(Guid videoMetadataId)
        {
            var videoMetadata = await this.storageBroker.SelectVideoMetadataByIdAsync(videoMetadataId);
            if (videoMetadata == null)
            {
                return null;
            }
            var bloburi = videoMetadata.BlobPath;
            string blobName = bloburi.Split('/').Last();
            return await blobBroker.GetBlobStreamAsync(blobName, "videos");
        }
    }
}
