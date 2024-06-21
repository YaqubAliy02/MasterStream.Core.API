using Xeptions;

namespace MasterStream.Core.API.Models.Videos.Exceptions
{
    public class InvalidVideoExceptions : Xeption
    {
        public InvalidVideoExceptions(string message)
            :base(message)
        { }
    }
}
