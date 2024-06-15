//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

using System.Linq.Expressions;
using System.Runtime.Serialization;
using MasterStream.Core.API.Brokers.Loggings;
using MasterStream.Core.API.Models.VideoMetadatas;
using MasterStream.Core.API.Models.VideoMetadatas.Brokers.Storages;
using MasterStream.Core.API.Services.VideoMetadatas;
using Microsoft.Data.SqlClient;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace MasterStream.Core.API.Tests.Unit.Services.Foundations.VideoMetadatas
{
    public partial class VideoMetadataServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly IVideoMetadataService videoMetadataService;

        public VideoMetadataServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.videoMetadataService = new VideoMetadataService(
                storageBroker: this.storageBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        private static string GetRandomString() =>
            new MnemonicString().GetValue().ToString();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private static VideoMetadata CreateRandomVideoMetadata() =>
            CreateVideoMetadataFiller(date: GetRandomDateTimeOffset()).Create();

        private static IQueryable<VideoMetadata> CreateRandomVideoMetadatas()
        {
            return CreateVideoMetadataFiller(date: GetRandomDateTimeOffset())
                .Create(count: GetRandomNumber()).AsQueryable();
        }

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: DateTime.UnixEpoch).GetValue();

        private static Filler<VideoMetadata> CreateVideoMetadataFiller(DateTimeOffset date)
        {
            var filler = new Filler<VideoMetadata>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(date);

            return filler;
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        private static SqlException GetSqlException() =>
            (SqlException)FormatterServices.GetSafeUninitializedObject(typeof(SqlException));
    }
}
