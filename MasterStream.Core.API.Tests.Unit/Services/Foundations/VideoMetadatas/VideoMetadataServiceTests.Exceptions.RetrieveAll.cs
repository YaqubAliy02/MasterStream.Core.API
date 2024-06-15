//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using FluentAssertions;
using MasterStream.Core.API.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Moq;

namespace MasterStream.Core.API.Tests.Unit.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public void ShouldThrowCriticalDependencyExceptionOnRetrieveAllWhenSqlExceptionOccursAndLogIt()
        {
            //given
            SqlException sqlException = GetSqlException();

            var failedVideoMetadataStorageException =
                new FailedVideoMetadataStorageException(
                    "Failed Video Metadata storage error occured, please contact support.",
                        sqlException);

            VideoMetadataDependencyException expectedVideoMetadataDependencyException =
                new VideoMetadataDependencyException(
                    "Video metadata dependency error occured, fix the errors and try again",
                        failedVideoMetadataStorageException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllVideoMetadatas()).Throws(sqlException);

            //when
            Action retrieveAllVideoMetadatasAction = () =>
                this.videoMetadataService.RetrieveAllVideoMetadatas();

            VideoMetadataDependencyException actualVideoMetadataDependencyException =
                Assert.Throws<VideoMetadataDependencyException>(retrieveAllVideoMetadatasAction);

            //then
            actualVideoMetadataDependencyException.Should().BeEquivalentTo(
                expectedVideoMetadataDependencyException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllVideoMetadatas(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCriticalError(It.Is(SameExceptionAs(expectedVideoMetadataDependencyException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceExceptionOnRetrieveAllIfServiceErrorOccursAndLogItAsync()
        {
            //given
            string exceptionMessage = GetRandomString();
            var serviceException = new Exception(exceptionMessage);

            FailedVideoMetadataServiceException failedVideoMetadataServiceException =
                new FailedVideoMetadataServiceException(
                    "Unexpected error of Video Metadata occured.",
                        serviceException);

            VideoMetadataServiceException expectedVideoMetadataServiceException =
                new VideoMetadataServiceException(
                    "Unexpected service error occured. Contact support.",
                        failedVideoMetadataServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllVideoMetadatas()).Throws(serviceException);

            //when
            Action retrieveAllVideoMetadatasAction = () =>
                this.videoMetadataService.RetrieveAllVideoMetadatas();

            VideoMetadataServiceException actualVideoMetadataServiceException =
                Assert.Throws<VideoMetadataServiceException>(retrieveAllVideoMetadatasAction);

            //then
            actualVideoMetadataServiceException.Should().BeEquivalentTo(
                expectedVideoMetadataServiceException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllVideoMetadatas(),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedVideoMetadataServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
