//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Models.Videos;
using MasterStream.Core.API.Models.Videos.Exceptions;
using MasterStream.Core.API.Services.Videos;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoController : RESTFulController
{
    private readonly IVideoService videoService;

    public VideoController(IVideoService videoService)
    {
        this.videoService = videoService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadMedia(IFormFile file)
    {
        if (file == null || !ValidateVideo(file))
        {
            throw new InvalidVideoExceptions("Video is not valid format.");
        }

        using var stream = file.OpenReadStream();
        var blobUri = await videoService.AddVideoAsync(stream, file.FileName, file.ContentType);

        var video = new Video
        {
            Id = Guid.NewGuid(),
            FileName = file.FileName,
            ContentType = file.ContentType,
            Size = file.Length,
            BlobUri = blobUri,
            UploadedDate = DateTime.UtcNow
        };

        return Ok(video);
    }

    private bool ValidateVideo(IFormFile file)
    {
        var allowedExtensions = new[] { ".mp4", ".avi", ".mov" };
        var extension = Path.GetExtension(file.FileName).ToLower();

        return file.Length > 0 && file.Length <= 50 * 1024 * 1024 && allowedExtensions.Contains(extension);
    }
}
