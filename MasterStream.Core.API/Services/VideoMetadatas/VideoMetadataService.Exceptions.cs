//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Models.Exceptions;
using MasterStream.Core.API.Models.VideoMetadatas;
using Xeptions;

namespace MasterStream.Core.API.Services.VideoMetadatas
{
    public partial class VideoMetadataService
    {
        private delegate ValueTask<VideoMetadata> ReturningVideoMetadataFunction();

        private async ValueTask<VideoMetadata> TryCatch(
            ReturningVideoMetadataFunction returningVideoMetadataFunction)
        {
            try
            {
                return await returningVideoMetadataFunction();
            }
            catch (NullVideoMetadataException nullVideoMetadataException)
            {
                throw CreateAndLogValidationExceptionAndIt(nullVideoMetadataException);
            }
            catch (InvalidVideoMetadataException invalidVideoMetadataException)
            {
                throw CreateAndLogValidationExceptionAndIt(invalidVideoMetadataException);
            }
        }

        private VideoMetadataValidationException CreateAndLogValidationExceptionAndIt(
            Xeption exception)
        {
            var videoMetadataValidationException =
                new VideoMetadataValidationException(
                message: "Video metadata validation error occured, fix errors and try again",
                innerException: exception);

            this.loggingBroker.LogError(videoMetadataValidationException);

            return videoMetadataValidationException;
        }
    }
}
