//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using FluentAssertions;
using MasterStream.Core.API.Models.VideoMetadatas;
using MasterStream.Core.API.Models.VideoMetadatas.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using STX.EFxceptions.Abstractions.Models.Exceptions;

namespace MasterStream.Core.API.Tests.Unit.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnAddIfSqlErrorOccursAndLogItAsync()
        {
            //given
            VideoMetadata someVideoMetadata = CreateRandomVideoMetadata();
            SqlException sqlException = GetSqlException();

            var failedVideoMetadataStorageException =
                new FailedVideoMetadataStorageException(
                    "Failed Video Metadata storage error occured, please contact support.",
                        sqlException);

            var expectedVideoMetadataDependencyException =
                new VideoMetadataDependencyException(
                    "Video metadata dependency error occured, fix the errors and try again.",
                        failedVideoMetadataStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset()).Throws(sqlException);

            //when
            ValueTask<VideoMetadata> AddVideoMetadataTask =
                this.videoMetadataService.AddVideoMetadataAsync(someVideoMetadata);

            VideoMetadataDependencyException actualVideoMetadataDependencyException =
                await Assert.ThrowsAsync<VideoMetadataDependencyException>(AddVideoMetadataTask.AsTask);

            //then
            actualVideoMetadataDependencyException.Should().BeEquivalentTo(expectedVideoMetadataDependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalError(It.Is(SameExceptionAs(expectedVideoMetadataDependencyException))),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowExceptionAddIfDublicateKeyErrorOccurs()
        {
            //given
            VideoMetadata someVideoMetadata = CreateRandomVideoMetadata();
            string someString = GetRandomString();

            var duplicateKeyException = new DuplicateKeyException(someString);

            var alreadyExistVideoMetadataException =
                new AlreadyExistsVideoMetadataException(
                    "Video Metadata already exist, please try again.",
                        duplicateKeyException);

            var expectedVideoMetadataDependencyValidationException
                = new VideoMetadataDependencyValidationException(
                    "Video Metadata dependency error occured. Fix errors and try again.",
                        alreadyExistVideoMetadataException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(duplicateKeyException);

            //when
            ValueTask<VideoMetadata> addVideoMetadataTask =
                this.videoMetadataService.AddVideoMetadataAsync(someVideoMetadata);

            VideoMetadataDependencyValidationException actualVideoMetadataDependencyValidationException =
                await Assert.ThrowsAnyAsync<VideoMetadataDependencyValidationException>(addVideoMetadataTask.AsTask);

            //then
            actualVideoMetadataDependencyValidationException.Should().BeEquivalentTo(
                expectedVideoMetadataDependencyValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once());

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataDependencyValidationException))),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAddIfDbCurrencyErrorOccursAndLogItAsync()
        {
            //given
            VideoMetadata someVideoMetadata = CreateRandomVideoMetadata();
            var dbUpdateConcurrencyException = new DbUpdateConcurrencyException();

            var lockedVideoMetadataException =
                new LockedVideoMetadataException("Video Metadata is locked, please try again.",
                    dbUpdateConcurrencyException);

            var expectedVideoMetadataDependencyValidationException =
                new VideoMetadataDependencyValidationException(
                    "Video Metadata dependency error occured. Fix errors and try again.",
                        lockedVideoMetadataException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset())
                    .Throws(dbUpdateConcurrencyException);

            //when
            ValueTask<VideoMetadata> addVideoMetadataTask =
                this.videoMetadataService.AddVideoMetadataAsync(someVideoMetadata);

            var actualVideoMetadataDependencyValidationException =
                await Assert.ThrowsAsync<VideoMetadataDependencyValidationException>(addVideoMetadataTask.AsTask);

            //then
            actualVideoMetadataDependencyValidationException.Should()
                .BeEquivalentTo(expectedVideoMetadataDependencyValidationException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataDependencyValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertVideoMetadataAsync(someVideoMetadata), Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAddIfDbUpdateErrorOccursAndLogItAsync()
        {
            //given
            VideoMetadata someVideoMetadata = CreateRandomVideoMetadata();
            var dbUpdateException = new DbUpdateException();

            var failedVideoMetadataStorageException = new FailedVideoMetadataStorageException(
                "Failed Video Metadata storage error occured, please contact support.",
                    dbUpdateException);

            var expectedVideoMetadataDependencyException = new VideoMetadataDependencyException(
                "Video metadata dependency exception error occured, please contact support.",
                    failedVideoMetadataStorageException);

            this.dateTimeBrokerMock.Setup(broker =>
               broker.GetCurrentDateTimeOffset())
                   .Throws(dbUpdateException);

            //when
            ValueTask<VideoMetadata> addVideoMetadataTask =
                this.videoMetadataService.AddVideoMetadataAsync(someVideoMetadata);

            VideoMetadataDependencyException actualVideoMetadataDependencyException =
                await Assert.ThrowsAsync<VideoMetadataDependencyException>(addVideoMetadataTask.AsTask);

            //then
            actualVideoMetadataDependencyException.Should().BeEquivalentTo(expectedVideoMetadataDependencyException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(), Times.Once);

            this.loggingBrokerMock.Verify(broker => broker.LogError(It.Is(
                SameExceptionAs(expectedVideoMetadataDependencyException))), Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateVideoMetadataAsync(It.IsAny<VideoMetadata>()), Times.Never);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowExceptionOnAddIfServiceErrorOccurs()
        {
            //given
            VideoMetadata someVideoMetadata = CreateRandomVideoMetadata();
            var exception = new Exception();

            var failedVideoMetadataServiceException =
                new FailedVideoMetadataServiceException(
                    "Unexpected error of Video Metadata occured",
                        exception);

            var expectedVideoMetadataDependencyServiceException =
                new VideoMetadataServiceException(
                    "Unexpected service error occured. Contact support.",
                        failedVideoMetadataServiceException);

            this.dateTimeBrokerMock.Setup(broker =>
                broker.GetCurrentDateTimeOffset()).Throws(exception);

            //when
            ValueTask<VideoMetadata> AddVideoMetadataTask =
                this.videoMetadataService.AddVideoMetadataAsync(someVideoMetadata);

            VideoMetadataServiceException actualVideoMetadataDependencyServiceException =
                await Assert.ThrowsAsync<VideoMetadataServiceException>(AddVideoMetadataTask.AsTask);

            //then
            actualVideoMetadataDependencyServiceException.Should()
                .BeEquivalentTo(expectedVideoMetadataDependencyServiceException);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.GetCurrentDateTimeOffset(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataDependencyServiceException))),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
