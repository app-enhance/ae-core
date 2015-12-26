namespace AE.Events.Tests
{
    using AE.Events.Handlers;

    using Xunit;

    public class EventHandlersTests
    {
        [Fact]
        public void EventHandlerBase_can_resolve_handle_methods_without_parameters_per_call()
        {
            // Arrange
            var eventHandler = new TestHandlers();

            // Act
            eventHandler.Handle(null);

            // Assert
            Assert.True(eventHandler.IsHandled);
        }


        private class TestHandlers : EventHandlerBase
        {
            public bool IsHandled { get; private set; }

            public void Handle()
            {
                IsHandled = true;
            }
        }
    }
}