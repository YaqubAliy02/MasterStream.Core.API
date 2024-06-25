using System.Net.Http.Headers;

namespace MasterStream.Core.Web.Services
{
    public class VideoMetadataWebService : IVideoMetadataWebService
    {
        private readonly HttpClient httpClient;

        public VideoMetadataWebService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> UploadVideoAsync(Stream videoStream, string videoFileName, string videoContentType)
        {
            using var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(videoStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(videoContentType);
            content.Add(streamContent, "file", videoFileName);

            var response = await httpClient.PostAsync("api/video/upload", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
