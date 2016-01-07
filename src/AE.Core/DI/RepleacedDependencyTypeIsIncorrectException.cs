namespace AE.Core.DI
{
    using System;

    public class RepleacedDependencyTypeIsIncorrectException : Exception
    {
        public RepleacedDependencyTypeIsIncorrectException()
        {
        }

        public RepleacedDependencyTypeIsIncorrectException(string message)
            : base(message)
        {
        }

        public RepleacedDependencyTypeIsIncorrectException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}