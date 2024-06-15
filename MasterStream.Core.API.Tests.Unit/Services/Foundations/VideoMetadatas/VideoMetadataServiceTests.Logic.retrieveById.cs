using FluentAssertions;
using Force.DeepCloner;
using MasterStream.Core.API.Models.VideoMetadatas;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterStream.Core.API.Tests.Unit.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveVideoMetadataByIdAsync()
        {
            //given
            Guid randomVideoMetadataId = Guid.NewGuid();
            Guid inputVideoMetadataId = randomVideoMetadataId;
            VideoMetadata randomVideoMetadata = CreateRandomVideoMetadata();
            VideoMetadata storageVideoMetadata = randomVideoMetadata;
            VideoMetadata expectedVideoMetadata = storageVideoMetadata.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.SelectVideoMetadataByIdAsync(inputVideoMetadataId))
                    .ReturnsAsync(storageVideoMetadata);

            //when
            VideoMetadata actualVideoMetadata =
                await this.videoMetadataService.RetrieveVideoMetadataByIdAsync(inputVideoMetadataId);

            //then
            actualVideoMetadata.Should().BeEquivalentTo(expectedVideoMetadata);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectVideoMetadataByIdAsync(inputVideoMetadataId), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.dateTimeBrokerMock.VerifyNoOtherCalls();
        }
    }
}
