//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Models.VideoMetadatas;

namespace MasterStream.Core.API.Services.VideoMetadatas
{
    internal interface IVideoMetadataService
    {
        ValueTask<VideoMetadata> AddVideoMetadataAsync(VideoMetadata videoMetadata);
        IQueryable<VideoMetadata> RetrieveAllVideoMetadatas();
        ValueTask<VideoMetadata> ModifyVideoMetadataAsync(VideoMetadata videoMetadata);
    }
}