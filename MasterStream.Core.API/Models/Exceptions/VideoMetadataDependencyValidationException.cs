//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream.Core.API.Models.Exceptions
{
    public class VideoMetadataDependencyValidationException : Xeption
    {
        public VideoMetadataDependencyValidationException(string message, Xeption innerException) 
            :base(message, innerException)
        { }
    }
}
