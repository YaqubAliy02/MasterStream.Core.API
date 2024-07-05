using Xeptions;

namespace MasterStream.Core.API.Models.Exceptions
{
    public class AlreadyExistsVideoMetadataException : Xeption
    {
        public AlreadyExistsVideoMetadataException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
