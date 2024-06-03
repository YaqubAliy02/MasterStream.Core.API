//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

namespace MasterStream.Core.API.Models.VideoMetadatas.Brokers.Storages
{
    internal interface IStorageBroker
    {
        ValueTask<VideoMetadata> InsertVideoMetadataAsync(VideoMetadata videoMetadata);
    }
}