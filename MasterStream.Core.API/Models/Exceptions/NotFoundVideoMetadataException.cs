//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using Xeptions;

namespace MasterStream.Core.API.Models.Exceptions
{
    public class NotFoundVideoMetadataException : Xeption
    {
        public NotFoundVideoMetadataException(string message)
            : base(message)
        { }
    }
}