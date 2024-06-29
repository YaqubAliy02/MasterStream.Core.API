//--------------------------
// TARTEEB LLC
// ALL RIGHTS RESERVED
//--------------------------

namespace MasterStream.Core.Web.Models.Photo
{
    public class Photo
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public string BlobUri { get; set; }
        public DateTimeOffset UploadedDate { get; set; }
    }
}
