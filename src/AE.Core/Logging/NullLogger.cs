namespace AE.Core.Logging
{
    using System;

    using DI;

    public class NullLogger : ILogger, INotRegisterDependency
    {
        public static ILogger Instance { get; } = new NullLogger();

        public void Error(string message)
        {
        }

        public void Error(string message, Exception ex)
        {
        }

        public void Warning(string message)
        {
        }

        public void Information(string message)
        {
        }

        public void Debug(string message)
        {
        }

        public void Debug(string message, object toSerialize)
        {
        }
    }
}