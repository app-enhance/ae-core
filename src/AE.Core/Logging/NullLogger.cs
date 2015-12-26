namespace AE.Core.Logging
{
    using System;

    using AE.Core.DI;

    public class NullLogger : ILogger, INotRegisterDependency
    {
        private static readonly ILogger instance = new NullLogger();

        public static ILogger Instance
        {
            get { return instance; }
        }

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