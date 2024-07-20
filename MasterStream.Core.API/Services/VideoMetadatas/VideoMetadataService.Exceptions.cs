//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using MasterStream.Core.API.Models.VideoMetadatas;
using MasterStream.Core.API.Models.VideoMetadatas.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using STX.EFxceptions.Abstractions.Models.Exceptions;
using Xeptions;

namespace MasterStream.Core.API.Services.VideoMetadatas
{
    public partial class VideoMetadataService
    {
        private delegate ValueTask<VideoMetadata> ReturningVideoMetadataFunction();
        private delegate IQueryable<VideoMetadata> ReturningVideoMetadatasFunction();

        private async ValueTask<VideoMetadata> TryCatch(
            ReturningVideoMetadataFunction returningVideoMetadataFunction)
        {
            try
            {
                return await returningVideoMetadataFunction();
            }
            catch (NullVideoMetadataException nullVideoMetadataException)
            {
                throw CreateAndLogValidationException(nullVideoMetadataException);
            }
            catch (InvalidVideoMetadataException invalidVideoMetadataException)
            {
                throw CreateAndLogValidationException(invalidVideoMetadataException);
            }
            catch (SqlException sqlException)
            {
                var failedVideoMetadataStorageException =
                    new FailedVideoMetadataStorageException(
                        message: "Failed Video Metadata storage error occured, please contact support.",
                        innerException: sqlException);

                throw CreateAndLogCriticalDependencyException(failedVideoMetadataStorageException);
            }
            catch (NotFoundVideoMetadataException notFoundVidoeMetadataException)
            {
                throw CreateAndLogValidationException(notFoundVidoeMetadataException);
            }
            catch (DuplicateKeyException duplicateKeyException)
            {
                var alreadyExistVideoMetadataException
                    = new AlreadyExistsVideoMetadataException(
                        "Video Metadata already exist, please try again.",
                            duplicateKeyException);

                throw CreateAndLogDuplicateKeyException(alreadyExistVideoMetadataException);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                var lockedVideoMetadataException = new LockedVideoMetadataException(
                    "Video Metadata is locked, please try again.",
                        dbUpdateConcurrencyException);

                throw CreateAndLogDependencyValidationException(lockedVideoMetadataException);
            }
            catch (DbUpdateException databaseUpdateException)
            {
                var failedVideoMetadataStorage = new FailedVideoMetadataStorageException(
                    "Failed Video Metadata storage error occured, please contact support.",
                        databaseUpdateException);

                throw CreateAndLogDependencyException(failedVideoMetadataStorage);
            }
            catch (Exception exception)
            {
                var failedVideoMetadataServiceException =
                    new FailedVideoMetadataServiceException(
                        "Unexpected error of Video Metadata occured",
                            exception);

                throw CreateAndLogVideoMetadataDependencyServiceErrorOccurs(failedVideoMetadataServiceException);
            }
        }

        private IQueryable<VideoMetadata> TryCatch(
            ReturningVideoMetadatasFunction returningVideoMetadatasFunction)
        {
            try
            {
                return returningVideoMetadatasFunction();
            }
            catch (SqlException sqlException)
            {
                FailedVideoMetadataStorageException failedVideoMetadataStorageException =
                    new FailedVideoMetadataStorageException(
                        "Failed Video Metadata storage error occured, please contact support.",
                            sqlException);
                throw CreateAndLogCriticalDependencyException(failedVideoMetadataStorageException);
            }
            catch (Exception exception)
            {
                FailedVideoMetadataServiceException failedVideoMetadataServiceException =
                    new FailedVideoMetadataServiceException(
                        "Unexpected error of Video Metadata occured.",
                            exception);

                throw CreateAndLogVideoMetadataServiceErrorOccurs(failedVideoMetadataServiceException);
            }
        }

        private VideoMetadataDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var videoMetadataDependencyException = new VideoMetadataDependencyException(
                "Video metadata dependency exception error occured, please contact support.",
                    exception);

            this.loggingBroker.LogError(videoMetadataDependencyException);

            return videoMetadataDependencyException;
        }

        private VideoMetadataDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var videoMetadataDependencyValidationException = new VideoMetadataDependencyValidationException(
                "Video Metadata dependency error occured. Fix errors and try again.",
                    exception);

            this.loggingBroker.LogError(videoMetadataDependencyValidationException);

            return videoMetadataDependencyValidationException;
        }

        private VideoMetadataDependencyValidationException CreateAndLogDuplicateKeyException(Xeption exception)
        {
            var videoMetadataDependencyValidationException =
                new VideoMetadataDependencyValidationException(
                    "Video Metadata dependency error occured. Fix errors and try again.",
                        exception);
            this.loggingBroker.LogError(videoMetadataDependencyValidationException);

            return videoMetadataDependencyValidationException;
        }

        private VideoMetadataServiceException CreateAndLogVideoMetadataDependencyServiceErrorOccurs(Xeption exception)
        {
            var videoMetadataDependencyServiceException =
                new VideoMetadataServiceException(
                    "Unexpected service error occured. Contact support.",
                        exception);

            this.loggingBroker.LogError(videoMetadataDependencyServiceException);

            return videoMetadataDependencyServiceException;
        }

        private VideoMetadataServiceException CreateAndLogVideoMetadataServiceErrorOccurs(Xeption exception)
        {
            var videoMetadataDependencyServiceException =
                new VideoMetadataServiceException(
                    "Unexpected service error occured. Contact support.",
                        exception);

            this.loggingBroker.LogError(videoMetadataDependencyServiceException);

            return videoMetadataDependencyServiceException;
        }

        private VideoMetadataValidationException CreateAndLogValidationException(
            Xeption exception)
        {
            var videoMetadataValidationException =
                new VideoMetadataValidationException(
                message: "Video metadata validation error occured, fix errors and try again",
                innerException: exception);

            this.loggingBroker.LogError(videoMetadataValidationException);

            return videoMetadataValidationException;
        }

        private VideoMetadataDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var videoMetadataDependencyException =
                new VideoMetadataDependencyException(
                    message: "Video metadata dependency error occured, fix the errors and try again.",
                    innerException: exception);

            this.loggingBroker.LogCriticalError(videoMetadataDependencyException);

            return videoMetadataDependencyException;
        }
    }
}
