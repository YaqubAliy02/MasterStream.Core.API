//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

namespace MasterStream.Core.API.Models.VideoMetadatas.Brokers.Storages
{
    public interface IStorageBroker
    {
        ValueTask<VideoMetadata> InsertVideoMetadataAsync(VideoMetadata videoMetadata);
        IQueryable<VideoMetadata> SelectAllVideoMetadatas();
        ValueTask<VideoMetadata> SelectVideoMetadataByIdAsync(Guid videometadataId);
        ValueTask<VideoMetadata> UpdateVideoMetadataAsync(VideoMetadata videoMetadata);
        ValueTask<VideoMetadata> DeleteVideoMetadataAsync(VideoMetadata videoMetadata);
    }
}