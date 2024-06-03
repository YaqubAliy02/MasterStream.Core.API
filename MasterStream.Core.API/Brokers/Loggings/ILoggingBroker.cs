//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

namespace MasterStream.Core.API.Brokers.Loggings
{
    public interface ILoggingBroker
    {
        void LogCriticalError(Exception exception);
        void LogError(Exception exception);
    }
}
