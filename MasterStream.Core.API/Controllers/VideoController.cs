//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Models.Videos;
using MasterStream.Core.API.Models.Videos.Exceptions;
using MasterStream.Core.API.Services.Videos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

[ApiController]
[EnableCors("AllowAll")]
[Route("api/[controller]")]
public class VideoController : RESTFulController
{
    private readonly IVideoService videoService;

    public VideoController(IVideoService videoService)
    {
        this.videoService = videoService;
    }

    [HttpPost("uploadvideo")]
    public async Task<IActionResult> UploadVideo(IFormFile file)
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

    [HttpGet("stream/{videoMetadataId}")]
    public async Task<IActionResult> StreamVideo(Guid videoMetadataId)
    {
        var stream = await this.videoService.GetVideoStreamByIdAsync(videoMetadataId);

        if (stream == null)
        {
            return NotFound();
        }

        var contentType = "video/mp4";
        return File(stream, contentType, enableRangeProcessing: true);
    }

    private bool ValidateVideo(IFormFile file)
    {
        var allowedExtensions = new[] { ".mp4", ".avi", ".mov" };
        var extension = Path.GetExtension(file.FileName).ToLower();

        return file.Length > 0 && allowedExtensions.Contains(extension);
    }
}
