//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream.Core.API.Models.Exceptions
{
    public class NullVideoMetadataException : Xeption
    {
        public NullVideoMetadataException()
            :base(message: "VideoMetadata is null")
        {}
    }
}
