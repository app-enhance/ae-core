namespace AE.Core.Logging
{
    using System;

    using Microsoft.Extensions.Logging;

    using Serialization;

    public class LoggerWithSerializationDecorator : ILogger
    {
        private readonly ILogger _logger;

        private readonly ISerializeService _serializeService;

        public LoggerWithSerializationDecorator(ILogger logger, ISerializeService serializeService)
        {
            _logger = logger;
            _serializeService = serializeService;
        }

        public void Log(LogLevel logLevel,
                        int eventId,
                        object state,
                        Exception exception,
                        Func<object, Exception, string> formatter)
        {
            _logger.Log(logLevel, eventId, state, exception, formatter);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }

        public IDisposable BeginScopeImpl(object state)
        {
            return _logger.BeginScopeImpl(state);
        }

        public async void Log(LogLevel logLevel,
                        int eventId,
                        object state,
                        Exception exception,
                        Func<object, Exception, string> formatter,
                        object toSerialize)
        {
            if (IsEnabled(logLevel) == false)
            {
                return;
            }

            var message = formatter != null ? formatter(state, exception) : LogFormatter.Formatter(state, exception);
            var serializedObject = await _serializeService.Serialize(toSerialize);

            Log(logLevel, eventId, $"{message}{Environment.NewLine}{serializedObject}", null, null);
        }
    }
}