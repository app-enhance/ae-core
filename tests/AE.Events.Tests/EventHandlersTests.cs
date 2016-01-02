namespace AE.Events.Tests
{
    using Handling;

    using Xunit;

    public class EventHandlersTests
    {
        [Fact]
        public void EventHandlerBase_can_resolve_handle_methods_by_matching_event_type()
        {
            // Arrange
            var eventHandler = new TestHandlers();
            var testEvent = new TestEvent() as IEvent;

            // Act
            eventHandler.Handle(testEvent);

            // Assert
            Assert.Equal(2, eventHandler.HandledCount);
        }

        [Fact]
        public void EventHandlerBase_can_resolve_different_handlers_by_type()
        {
            // Arrange
            var eventHandler = new TestHandlers();
            var eventHandler2 = new TestHandlers2();
            var testEvent = new TestEvent() as IEvent;

            // Act
            eventHandler.Handle(testEvent);
            eventHandler2.Handle(testEvent);

            // Assert
            Assert.Equal(2, eventHandler.HandledCount);
            Assert.Equal(2, eventHandler2.HandledCount);
        }

        private class TestHandlers : EventHandlerBase, IEventHandler<TestEvent>
        {
            public int HandledCount { get; private set; }

            public void Handle(TestEvent @event)
            {
                HandledCount++;
            }

            public override void Handle(IEvent @event)
            {
                base.Handle(@event);
                HandledCount++;
            }
        }

        private class TestHandlers2 : EventHandlerBase, IEventHandler<TestEvent>
        {
            public int HandledCount { get; private set; }

            public void Handle(TestEvent @event)
            {
                HandledCount++;
            }

            public override void Handle(IEvent @event)
            {
                base.Handle(@event);
                HandledCount++;
            }
        }

        private class TestEvent : IEvent
        {
        }
    }
}