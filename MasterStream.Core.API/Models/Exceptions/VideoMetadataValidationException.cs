//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream.Core.API.Models.Exceptions
{
    public class VideoMetadataValidationException : Xeption
    {
        public VideoMetadataValidationException(Xeption innerException)
            :base(message: "VideoMetadata validation error occured, fix errors and try again",
                 innerException)
        { }
    }
}
