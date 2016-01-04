namespace AE.Core.Tests
{
    using System;

    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Internal;

    using Xunit;

    /// <summary>
    ///     This class tests exact LogFormatter.Formatter(state, exception) method
    ///     but will never used outside Logger that's why whole Logger is tested.
    /// </summary>
    public class LearningMicrosoftLoggerTests
    {
        [Fact]
        public void When_pass_only_string_state_param_to_log_then_message_equals_state()
        {
            // Arrange
            var loggerProvider = new MessageVariableLoggerProvider();
            var logger = CreateLogger(loggerProvider);

            // Act
            logger.Log(LogLevel.Error, 0, "test", null, null);

            // Assert
            Assert.Equal("test", loggerProvider.Message);
        }

        [Fact]
        public void When_pass_only_exception_param_to_log_then_message_equals_eol_with_StackTrace()
        {
            // Arrange
            var loggerProvider = new MessageVariableLoggerProvider();
            var logger = CreateLogger(loggerProvider);
            var exception = GetThrowedException("test");

            // Act
            logger.Log(LogLevel.Error, 0, null, exception, null);

            // Assert
            Assert.StartsWith($"{Environment.NewLine}System.Exception: test", loggerProvider.Message);
        }

        [Fact]
        public void When_pass_state_and_exception_params_to_log_then_message_equals_state_with_StackTrace()
        {
            // Arrange
            var loggerProvider = new MessageVariableLoggerProvider();
            var logger = CreateLogger(loggerProvider);
            var exception = GetThrowedException("test");

            // Act
            logger.Log(LogLevel.Error, 0, "test", exception, null);

            // Assert
            Assert.StartsWith($"test{Environment.NewLine}System.Exception: test", loggerProvider.Message);
        }

        [Fact]
        public void When_pass_object_state_param_to_log_then_message_equals_object_toString()
        {
            // Arrange
            var loggerProvider = new MessageVariableLoggerProvider();
            var logger = CreateLogger(loggerProvider);

            // Act
            logger.Log(LogLevel.Error, 0, new TestObject(), null, null);

            // Assert
            Assert.Equal("AE.Core.Tests.LearningMicrosoftLoggerTests+TestObject", loggerProvider.Message);
        }

        [Fact]
        public void
            When_pass_ILogValues_state_param_to_log_then_message_equals_to_wierd_toString_format_usefull_for_describe_types
            ()
        {
            // Arrange
            var loggerProvider = new MessageVariableLoggerProvider();
            var logger = CreateLogger(loggerProvider);

            // Act
            logger.Log(
                LogLevel.Error,
                0,
                new FormattedLogValues($"{{{nameof(TestObject)}}}{{string}}", new TestObject(), "test_string"),
                null,
                null);

            // Assert
            Assert.Equal(
                "TestObject: AE.Core.Tests.LearningMicrosoftLoggerTests+TestObject  string: test_string  {OriginalFormat}: {TestObject}{string}",
                loggerProvider.Message);
        }

        private static Exception GetThrowedException(string message)
        {
            try
            {
                throw new Exception(message);
            }
            catch (Exception e)
            {
                return e;
            }
        }

        private static ILogger CreateLogger(ILoggerProvider loggerProvider)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(loggerProvider);
            loggerFactory.MinimumLevel = LogLevel.Verbose;
            return loggerFactory.CreateLogger("LearnLogger");
        }

        private class MessageVariableLoggerProvider : ILoggerProvider
        {
            public string Message { get; set; }

            public ILogger CreateLogger(string categoryName)
            {
                return new MessageVariableLogger(SetMessageVariable);
            }

            public void Dispose()
            {
            }

            private void SetMessageVariable(string message)
            {
                Message = message;
            }
        }

        private class MessageVariableLogger : ILogger
        {
            private readonly Action<string> _setMessageVariable;

            public MessageVariableLogger(Action<string> setMessageVariable)
            {
                _setMessageVariable = setMessageVariable;
            }

            public void Log(LogLevel logLevel,
                            int eventId,
                            object state,
                            Exception exception,
                            Func<object, Exception, string> formatter)
            {
                var message = formatter != null ? formatter(state, exception) : LogFormatter.Formatter(state, exception);
                _setMessageVariable(message);
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
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

        private class TestObject
        {
            public string Property { get; set; }
        }
    }
}