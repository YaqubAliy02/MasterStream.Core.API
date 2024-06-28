//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Services.Photos;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using MasterStream.Core.API.Models.Photos;
using MasterStream.Core.API.Models.Photos.Exceptions;
using Microsoft.AspNetCore.Cors;

namespace MasterStream.Core.API.Controllers
{
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class PhotoController : RESTFulController
    {
        private readonly IPhotoService photoService;

        public PhotoController(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        [HttpPost("uploadphoto")]
        public async Task<IActionResult> UploadPhoto(IFormFile file)
        {
            if (file == null || !ValidatePhoto(file))
            {
                throw new InvalidPhotoExceptions("Photo is not valid format.");
            }

            using var stream = file.OpenReadStream();
            var blobUri = await photoService.AddPhotoAsync(stream, file.FileName, file.ContentType);

            var photo = new Photo
            {
                Id = Guid.NewGuid(),
                FileName = file.FileName,
                ContentType = file.ContentType,
                Size = file.Length,
                BlobUri = blobUri,
                UploadedDate = DateTime.UtcNow
            };

            return Ok(photo);
        }

        private bool ValidatePhoto(IFormFile file)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            return file.Length > 0 && allowedExtensions.Contains(extension);
        }
    }
}
