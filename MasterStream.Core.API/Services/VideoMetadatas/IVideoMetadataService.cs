//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Models.VideoMetadatas;

namespace MasterStream.Core.API.Services.VideoMetadatas
{
    public interface IVideoMetadataService
    {
        ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata);
        ValueTask<VideoMetadata> RetrieveVideoMetadataByIdAsync(Guid videoMetadataId);
        IQueryable<VideoMetadata> RetrieveAllVideoMetadatas();
        ValueTask<VideoMetadata> ModifyVideoMetadataAsync(VideoMetadata videoMetadata);
        ValueTask<VideoMetadata> RemoveVideoMetadataByIdAsync(Guid videoMetadataId);
    }
}