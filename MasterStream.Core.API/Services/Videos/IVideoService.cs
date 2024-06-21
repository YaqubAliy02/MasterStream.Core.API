using MasterStream.Core.API.Models.Videos;

namespace MasterStream.Core.API.Services.Videos
{
    public interface IVideoService
    {
        Task<string> AddVideoAsync(Stream fileStream, string fileName, string contentType);
        Task<List<Video>> RetrieveAllVideosAsync();
    }
}
