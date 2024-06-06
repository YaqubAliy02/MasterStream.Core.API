//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Microsoft.EntityFrameworkCore;

namespace MasterStream.Core.API.Models.VideoMetadatas.Brokers.Storages
{
    public partial class StorageBroker
    {
        private DbSet<VideoMetadata> VideoMetadatas { get; set; }

        public async ValueTask<VideoMetadata> InsertVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await InsertAsync(videoMetadata);

        public IQueryable<VideoMetadata> SelectAllVideoMetadatas() =>
            SelectAll<VideoMetadata>();

        public async ValueTask<VideoMetadata> SelectVideoMetadataByIdAsync(Guid videometadataId) =>
            await SelectAsync<VideoMetadata>(videometadataId);

        public async ValueTask<VideoMetadata> DeleteVideoMetadataAsync(VideoMetadata videoMetadata) =>
            await DeleteAsync(videoMetadata);
    }
}
