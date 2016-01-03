namespace AE.Events.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Handling;

    using Xunit;

    public class DispatcherTests
    {
        [Fact]
        public void EventDispatcher_swallow_exception_when_handler_throw()
        {
            // Arrange
            var handler = new TestEventHandler();
            var handlerFactory = new StubEventHandlerFactory().AddHandler(handler);
            var dispatcher = new DefaultEventDispatcher(handlerFactory);

            // Act
            dispatcher.Dispatch(new TestEvent());

            // Assert
            Assert.True(handler.IsHandled);
        }

        private class TestEventHandler : IEventHandler<TestEvent>
        {
            public bool IsHandled { get; set; }

            public void Handle(TestEvent @event)
            {
                IsHandled = true;
                throw new NotImplementedException();
            }
        }

        private class TestEvent : IEvent
        {
        }

        private class StubEventHandlerFactory : IEventHandlerFactory
        {
            private readonly List<KeyValuePair<Type, IEventHandler>> container;

            public StubEventHandlerFactory()
            {
                container = new List<KeyValuePair<Type, IEventHandler>>();
            }

            public IEnumerable<IEventHandler<T>> SearcHandlers<T>() where T : IEvent
            {
                var lookup = container.ToLookup(k => k.Key, v => v.Value);
                return lookup[typeof(T)].Cast<IEventHandler<T>>();
            }

            public IEnumerable<IEventHandler> GetAllEventHandlers()
            {
                return container.Select(x => x.Value).ToArray();
            }

            public StubEventHandlerFactory AddHandler<T>(IEventHandler<T> handler) where T : IEvent
            {
                container.Add(new KeyValuePair<Type, IEventHandler>(typeof(T), handler));
                return this;
            }
        }
    }
}