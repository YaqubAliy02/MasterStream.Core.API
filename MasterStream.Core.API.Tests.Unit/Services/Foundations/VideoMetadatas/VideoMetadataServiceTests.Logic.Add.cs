//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using FluentAssertions;
using Force.DeepCloner;
using MasterStream.Core.API.Models.VideoMetadatas;
using Moq;

namespace MasterStream.Core.API.Tests.Unit.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]

        public async Task ShouldAddVideoMetadataAsync()
        {
            //given
            VideoMetadata randomVideoMetadata = CreateRandomVideoMetadata();
            VideoMetadata inputVideoMetadata = randomVideoMetadata;
            VideoMetadata storageVideoMetadata = inputVideoMetadata;
            VideoMetadata expectedVideoMetadata = storageVideoMetadata.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertVideoMetadataAsync(inputVideoMetadata))
                .ReturnsAsync(storageVideoMetadata);

            //when
            VideoMetadata actualVideoMetadata =
                await this.videoMetadataService.AddVideoMetadataAsync(inputVideoMetadata);

            //then
            actualVideoMetadata.Should().BeEquivalentTo(expectedVideoMetadata);   

            this.storageBrokerMock.Verify(broker =>
                broker.InsertVideoMetadataAsync(inputVideoMetadata),
                Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();

        }


    }
}
