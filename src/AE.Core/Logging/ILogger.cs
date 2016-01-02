namespace AE.Core.Logging
{
    using System;

    using DI;

    public interface ILogger : IDependency
    {
        void Error(string message);

        void Error(string message, Exception ex);

        void Warning(string message);

        void Information(string message);

        void Debug(string message);

        void Debug(string message, object toSerialize);
    }
}