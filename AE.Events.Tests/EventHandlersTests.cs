namespace AE.Events.Tests
{
    using AE.Events.Handlers;

    using Xunit;

    public class EventHandlersTests
    {
        [Fact]
        public void EventHandlerBase_can_resolve_handlers_without_parameters()
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