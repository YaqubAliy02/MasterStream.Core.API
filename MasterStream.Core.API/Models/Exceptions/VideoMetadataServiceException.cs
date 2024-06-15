using Xeptions;

namespace MasterStream.Core.API.Models.Exceptions
{
    public class VideoMetadataServiceException : Xeption
    {
        public VideoMetadataServiceException(string message, Xeption innerException)
            : base(message, innerException)
        { }
    }
}
