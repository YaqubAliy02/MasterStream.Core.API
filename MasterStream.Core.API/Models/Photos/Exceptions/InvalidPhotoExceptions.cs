using Xeptions;

namespace MasterStream.Core.API.Models.Photos.Exceptions
{
    public class InvalidPhotoExceptions : Xeption
    {
        public InvalidPhotoExceptions(string message)
            : base(message) { }
    }
}
