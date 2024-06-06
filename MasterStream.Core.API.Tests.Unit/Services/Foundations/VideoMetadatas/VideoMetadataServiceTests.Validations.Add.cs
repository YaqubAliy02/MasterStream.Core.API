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
        public async  Task ShouldThrowValidationExceptionOnAddIfIsNullAndLogError()
        {   
            //given
            VideoMetadata nullVideoMetadata = null;
            var nullVideoMetadataException = new NullVideoMetadataException(
                message: "Video metadata is null");

            var expectedVideoMetadataValidationException = 
                new VideoMetadataValidationException(
                    message: "Video metadata validation error occured, fix errors and try again",
                    innerException:nullVideoMetadataException);

            //when
            ValueTask<VideoMetadata> addVideoMetadataTask = 
                this.videoMetadataService.AddVideoMetadataAsync(nullVideoMetadata);

            var actualVideoMetadataValidationException =
               await Assert.ThrowsAsync<VideoMetadataValidationException>(
                    addVideoMetadataTask.AsTask);

            //then
            actualVideoMetadataValidationException.Should()
                .BeEquivalentTo(expectedVideoMetadataValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                expectedVideoMetadataValidationException))),
                Times.Once);

            this.storageBrokerMock.Verify(broker => 
                broker.InsertVideoMetadataAsync(It.IsAny<VideoMetadata>()),
                Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionOnAddIfInputIsInvalidAndLogItAsync(
            string invalidText)
        {
            //given
            var invalidVideoMetadata = new VideoMetadata
            {
                Title = invalidText,
            };

            var invalidVideoMetadataException =
                new InvalidVideoMetadataException(
                    message: "Video metadata is invalid");

            invalidVideoMetadataException.AddData(
                key: nameof(VideoMetadata.Id),
                values: "Id is required");

            invalidVideoMetadataException.AddData(
                key: nameof(VideoMetadata.Title),
                values: "Text is required");

            invalidVideoMetadataException.AddData(
                key: nameof(VideoMetadata.BlobPath),
                values: "Text is required");

            invalidVideoMetadataException.AddData(
                key: nameof(VideoMetadata.CreatedAt),
                values: "Date is required");

            invalidVideoMetadataException.AddData(
                key: nameof(VideoMetadata.UpdatedDate),
                values: "Date is required");

            var expectedVideoMetadataValidationException =
                new VideoMetadataValidationException(
                    message: "Video metadata validation error occured, fix errors and try again",
                    innerException: invalidVideoMetadataException);

            //when
            ValueTask<VideoMetadata> addVideoMetadataTask =
                this.videoMetadataService.AddVideoMetadataAsync(invalidVideoMetadata);

            var actualVideoMetadataValidationException =
                await Assert.ThrowsAsync<VideoMetadataValidationException>(addVideoMetadataTask.AsTask);

            //then
             actualVideoMetadataValidationException.Should().BeEquivalentTo(
                 expectedVideoMetadataValidationException);

            this.loggingBrokerMock.Verify(broker => 
                broker.LogError(It.Is(SameExceptionAs(
                    expectedVideoMetadataValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertVideoMetadataAsync(It.IsAny<VideoMetadata>()), 
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.storageBrokerMock. VerifyNoOtherCalls();
        }
    }
}
