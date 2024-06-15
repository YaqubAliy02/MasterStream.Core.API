//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using FluentAssertions;
using MasterStream.Core.API.Models.VideoMetadatas;
using Moq;

namespace MasterStream.Core.API.Tests.Unit.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public void ShouldReturnVideoMetadatas()
        {
            //given
            IQueryable<VideoMetadata> randomVideoMetadatas = CreateRandomVideoMetadatas();
            IQueryable<VideoMetadata> storageVideoMetadatas = randomVideoMetadatas;
            IQueryable<VideoMetadata> expectedVideoMetadatas = storageVideoMetadatas;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectAllVideoMetadatas()).Returns(storageVideoMetadatas);

            //when
            IQueryable<VideoMetadata> actualVideoMetadata =
                this.videoMetadataService.RetrieveAllVideoMetadatas();

            //then
            actualVideoMetadata.Should().BeEquivalentTo(expectedVideoMetadatas);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectAllVideoMetadatas(), Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
