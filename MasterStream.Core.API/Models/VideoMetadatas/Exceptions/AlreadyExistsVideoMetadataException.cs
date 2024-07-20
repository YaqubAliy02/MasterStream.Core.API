using Xeptions;

namespace MasterStream.Core.API.Models.VideoMetadatas.Exceptions
{
    public class AlreadyExistsVideoMetadataException : Xeption
    {
        public AlreadyExistsVideoMetadataException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
