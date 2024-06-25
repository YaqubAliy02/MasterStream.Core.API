namespace MasterStream.Web.Services
{
    public class VideoMetadataService : IVideoMetadataService
    {
        private readonly HttpClient httpClient;

        public VideoMetadataService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> UploadVideoAsync(Stream videoStream, string videoFileName, string videoContentType)
        {
            using var content = new MultipartFormDataContent();
            content.Add(new StreamContent(videoStream), "file", videoFileName);

            var response = await httpClient.PostAsync("api/upload", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

    }
}
