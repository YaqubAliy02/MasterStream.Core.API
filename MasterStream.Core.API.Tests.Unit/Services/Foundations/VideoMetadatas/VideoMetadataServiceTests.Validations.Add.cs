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
            var nullVideoMetadataException = new NullVideoMetadataException();

            var expectedVideoMetadataValidationException = 
                new VideoMetadataValidationException(nullVideoMetadataException);

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
    }
}
