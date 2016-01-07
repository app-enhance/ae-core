namespace AE.Core.Logging
{
    using System;

    using Microsoft.Extensions.Logging;

    public class NullLogger : ILogger
    {
        public static ILogger Instance { get; } = new NullLogger();

        public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public IDisposable BeginScopeImpl(object state)
        {
            return new NullScope();
        }

        private class NullScope : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}