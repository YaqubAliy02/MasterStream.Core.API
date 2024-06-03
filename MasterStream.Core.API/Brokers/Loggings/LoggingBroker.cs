//--------------------------
// TARTEEB LLC               
// ALL RIGHTS RESERVED      
//--------------------------

namespace MasterStream.Core.API.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> logger)
        {
            this.logger = logger;
        }

        public void LogCriticalError(Exception exception) =>
            this.logger.LogCritical(exception.Message);

        public void LogError(Exception exception) =>
            this.logger.LogError(exception.Message);
       
    }
}
