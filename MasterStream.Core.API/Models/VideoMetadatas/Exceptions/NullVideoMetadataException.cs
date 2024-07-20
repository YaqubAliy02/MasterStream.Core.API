//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream.Core.API.Models.VideoMetadatas.Exceptions
{
    public class NullVideoMetadataException : Xeption
    {
        public NullVideoMetadataException(string message)
            : base(message)
        { }
    }
}
