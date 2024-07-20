//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream.Core.API.Models.VideoMetadatas.Exceptions
{
    public class VideoMetadataValidationException : Xeption
    {
        public VideoMetadataValidationException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
