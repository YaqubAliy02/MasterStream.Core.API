//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using FluentAssertions;
using MasterStream.Core.API.Models.Exceptions;
using MasterStream.Core.API.Models.VideoMetadatas;
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
                broker.GetCurrentDateTimeOffset()).Throws(duplicateKeyException);

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
    }
}
