//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream.Core.API.Models.VideoMetadatas.Exceptions
{
    public class LockedVideoMetadataException : Xeption
    {
        public LockedVideoMetadataException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
