using Xeptions;

namespace MasterStream.Core.API.Models.VideoMetadatas.Exceptions
{
    public class FailedVideoMetadataServiceException : Xeption
    {
        public FailedVideoMetadataServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
