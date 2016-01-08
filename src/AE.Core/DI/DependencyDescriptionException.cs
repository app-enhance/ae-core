﻿namespace AE.Core.DI
{
    using System;

    public class DependencyDescriptionException : Exception
    {

        public DependencyDescriptionException()
        {
        }

        public DependencyDescriptionException(string message)
            : base(message)
        {
        }

        public DependencyDescriptionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}