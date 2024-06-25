namespace MasterStream.Core.Web.Services
{
    public interface IVideoMetadataWebService
    {
        Task<string> UploadVideoAsync(Stream videoStream, string videoFileName, string videoContentType);
    }
}
