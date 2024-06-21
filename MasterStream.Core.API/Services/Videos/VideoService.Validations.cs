using MasterStream.Core.API.Models.Exceptions;
using MasterStream.Core.API.Models.VideoMetadatas;
using MasterStream.Core.API.Models.Videos;
using MasterStream.Core.API.Models.Videos.Exceptions;

namespace MasterStream.Core.API.Services.Videos
{
    public partial class VideoService
    {
        public void ValidateVideo(IFormFile file)
        {
            if(file == null || !ValidateVideoExtensions(file))
            {
                throw new InvalidVideoExceptions("Video is not valid format.");
            }
        }
        public bool ValidateVideoExtensions(IFormFile file)
        {
            var allowedExtensions = new[] { ".mp4", ".avi", ".mov" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            return file.Length > 0 && file.Length <= 50 * 1024 * 1024 && allowedExtensions.Contains(extension);
        }
    }
}
