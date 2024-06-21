namespace MasterStream.Core.API.Services.Videos
{
    public interface IVideoService
    {
        Task<string> AddVideoAsync(Stream fileStream, string fileName, string contentType);
    }
}
