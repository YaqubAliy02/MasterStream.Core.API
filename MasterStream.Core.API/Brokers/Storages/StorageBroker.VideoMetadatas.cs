//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Microsoft.EntityFrameworkCore;

namespace MasterStream.Core.API.Models.VideoMetadatas.Brokers.Storages
{
    internal partial class StorageBroker
    {
        private DbSet<VideoMetadata> VideoMetadatas {  get; set; }
    }
}
