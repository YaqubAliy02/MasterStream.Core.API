namespace MasterStream.Web.Services
{
    public interface IVideoMetadataService
    {
        Task<string> UploadVideoAsync(Stream videoStream, string videoFileName, string videoContentType);
    }
}
