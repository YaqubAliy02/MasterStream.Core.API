using MasterStream.Core.API.Models.Photos;

namespace MasterStream.Core.Web.Services
{
    public class PhotoService
    {
        private readonly HttpClient _httpClient;

        public PhotoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Photo>> GetPhotosAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<Photo>>("api/videometadata");
            return response;
        }
    }

}
