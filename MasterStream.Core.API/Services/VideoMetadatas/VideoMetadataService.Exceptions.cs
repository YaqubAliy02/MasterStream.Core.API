//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Models.Exceptions;
using MasterStream.Core.API.Models.VideoMetadatas;

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
            catch(NullVideoMetadataException nullVideoMetadataException)
            {
                throw CreateAndLogValidationExceptionIt(nullVideoMetadataException);
            }
        }

        private VideoMetadataValidationException CreateAndLogValidationExceptionIt(
            NullVideoMetadataException nullVideoMetadataException)
        {
            var videoMetaDataValidationException =
                new VideoMetadataValidationException(nullVideoMetadataException);

            this.loggingBroker.LogError(videoMetaDataValidationException);

            return videoMetaDataValidationException;
        }
    }
}
