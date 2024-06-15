
namespace MasterStream.Core.API.Brokers.DateTimes
{
    public class DateTimeBroker : IDateTimeBroker   
    {
        public DateTimeOffset GetCurrentDateTimeOffset() =>
            DateTimeOffset.UtcNow;
    }
}
