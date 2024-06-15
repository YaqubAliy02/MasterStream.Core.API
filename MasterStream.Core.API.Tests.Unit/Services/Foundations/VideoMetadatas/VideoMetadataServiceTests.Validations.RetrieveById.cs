//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using FluentAssertions;
using MasterStream.Core.API.Models.Exceptions;
using MasterStream.Core.API.Models.VideoMetadatas;
using Moq;

namespace MasterStream.Core.API.Tests.Unit.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogItAsync()
        {
            //given
            var invalidVideoMetadataId = Guid.Empty;

            var invalidVideoMetadataException =
                new InvalidVideoMetadataException("Video metadata is invalid");

            invalidVideoMetadataException.AddData(
                key: nameof(VideoMetadata.Id),
                values: "Id is required");

            var expectedVideoMetadataValidationException =
                new VideoMetadataValidationException(
                    "Video metadata validation error occured, fix errors and try again",
                        invalidVideoMetadataException);

            //when
            ValueTask<VideoMetadata> retrieveByIdVideoMetadataTask =
                this.videoMetadataService.RetrieveVideoMetadataByIdAsync(invalidVideoMetadataId);

            VideoMetadataValidationException actualVideoMetadataValidationException =
                await Assert.ThrowsAsync<VideoMetadataValidationException>(
                    retrieveByIdVideoMetadataTask.AsTask);

            //then
            actualVideoMetadataValidationException.Should().BeEquivalentTo(
                expectedVideoMetadataValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowNotFoundExceptionOnRetrieveByIdIfVideoMetadataIsNotFoundAndLogItAsync()
        {
            //given
            Guid someVideoMetadataId = Guid.NewGuid();
            VideoMetadata noVideoMetadata = null;

            NotFoundVideoMetadataException notFoundVidoeMetadataException =
                new NotFoundVideoMetadataException($"Couldn't find video metadata with id {someVideoMetadataId}");

            VideoMetadataValidationException expectedVideoMetadataValidationException =
                new VideoMetadataValidationException(
                    message: "Video metadata validation error occured, fix errors and try again",
                        notFoundVidoeMetadataException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()))
                    .ReturnsAsync(noVideoMetadata);

            //when
            ValueTask<VideoMetadata> retrieveByIdVideoMetadataTask =
                this.videoMetadataService.RetrieveVideoMetadataByIdAsync(someVideoMetadataId);

            VideoMetadataValidationException actualVideoMetadataValidationException =
                await Assert.ThrowsAsync<VideoMetadataValidationException>(
                    retrieveByIdVideoMetadataTask.AsTask);

            //then
            actualVideoMetadataValidationException.Should().BeEquivalentTo(
                expectedVideoMetadataValidationException);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectVideoMetadataByIdAsync(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedVideoMetadataValidationException))),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
