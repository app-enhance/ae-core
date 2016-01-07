namespace AE.Core.Logging
{
    using System;

    using Microsoft.Extensions.Logging;

    public static class LoggerExtensions
    {
        public static void LogDebug(this ILogger logger, string data, object toSerialize)
        {
            var loggerWithSerialization = logger as LoggerWithSerializationDecorator;
            if (loggerWithSerialization != null)
            {
                loggerWithSerialization.Log(LogLevel.Debug, 0, data, null, MessageFormatter);
            }
            else
            {
                logger.LogDebug(data);
            }
        }

        private static string MessageFormatter(object state, Exception exception)
        {
            if ((state == null) && (exception == null))
            {
                throw new InvalidOperationException("No message or exception details were found to create a message for the log.");
            }

            if (state == null)
            {
                return exception.ToString();
            }

            if (exception == null)
            {
                return state.ToString();
            }

            return $"{state}{Environment.NewLine}{exception}";
        }
    }
}