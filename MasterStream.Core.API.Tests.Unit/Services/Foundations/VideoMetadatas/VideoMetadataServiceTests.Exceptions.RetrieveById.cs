//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using FluentAssertions;
using MasterStream.Core.API.Models.VideoMetadatas;
using MasterStream.Core.API.Models.VideoMetadatas.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;

namespace MasterStream.Core.API.Tests.Unit.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldThrowCriticalDependencyExceptionOnRetrieveByIdIfsqlErrorsOccursAndLogItAsync()
        {
            //given
            Guid someId = Guid.NewGuid();
            SqlException sqlException = GetSqlException();

            FailedVideoMetadataStorageException failedVideoMetadataStorageException =
                new FailedVideoMetadataStorageException(
                    "Failed Video Metadata storage error occured, please contact support.",
                        sqlException);

            var expectedVideoMetadataDependencyException =
                new VideoMetadataDependencyException(
                    "Video metadata dependency error occured, fix the errors and try again.",
                        failedVideoMetadataStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(sqlException);

            //when
            ValueTask<VideoMetadata> retrieveVideoMetadataByIdTask =
                this.videoMetadataService.RetrieveVideoMetadataByIdAsync(someId);

            VideoMetadataDependencyException actualVideoMetadataDependencyException =
                await Assert.ThrowsAsync<VideoMetadataDependencyException>(
                    retrieveVideoMetadataByIdTask.AsTask);

            //then
            actualVideoMetadataDependencyException.Should().BeEquivalentTo(
                expectedVideoMetadataDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalError(It.Is(SameExceptionAs(expectedVideoMetadataDependencyException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnRetrieveByIdIfDatabaseUpdateErrorOccursAndLogItAsync()
        {
            //given
            Guid someId = Guid.NewGuid();
            var serviceException = new Exception();

            FailedVideoMetadataServiceException failedVideoMetadataServiceException =
                new FailedVideoMetadataServiceException(
                    "Unexpected error of Video Metadata occured",
                        serviceException);

            VideoMetadataServiceException expectedVideoMetadataServiceException =
                new VideoMetadataServiceException(
                    "Unexpected service error occured. Contact support.",
                        failedVideoMetadataServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()))
                    .ThrowsAsync(serviceException);

            //when
            ValueTask<VideoMetadata> retrieveVideoMetadataByIdTask =
                this.videoMetadataService.RetrieveVideoMetadataByIdAsync(someId);

            VideoMetadataServiceException actualVideoMetadataServiceException =
                await Assert.ThrowsAsync<VideoMetadataServiceException>(
                    retrieveVideoMetadataByIdTask.AsTask);

            //then
            actualVideoMetadataServiceException.Should().BeEquivalentTo(
                expectedVideoMetadataServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedVideoMetadataServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
