namespace AE.Events.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using AE.Core.Serialization;
    using AE.Events;
    using AE.Events.Handlers;
    using AE.Events.Services;

    using Newtonsoft.Json;

    using Xunit;

    public class EventsTests
    {
        public static readonly IList<string> EventExecuted = new List<string>();

        private IEventsPublisher eventPublisher;

        private IEventBus eventBus;

        private IEventDispatcher eventDispatcher;

        private IEventHandlerFactory eventHandlerFactory;

        public void Init()
        {
            EventExecuted.Clear();
            EventsQueue.ClearAll();
            TestEventHandlerWithThrowEx.ThrowedException = false;
        }

        [Fact]
        public void EventHandlerFactory_resolve_events()
        {
            // Arrange
            
            // Act
            var allEventhandlers = this.eventHandlerFactory.GetAllEventHandlers();
            var testEventsHandlers = this.eventHandlerFactory.SearcHandlers<TestEvent>();


            // Assert
            Assert.Equal(3, allEventhandlers.Count());
            Assert.Equal(2, testEventsHandlers.Count());
            // Assert.Equal(2, testDomainEventsHandlers1.Count());
            // Assert.Equal(1, testDomainEventsHandlers2.Count());
        }

        [Fact]
        public void Dispatch_Event()
        {
            // Arrange
            var event1 = new TestEvent();

            // Act
            this.eventDispatcher.Dispatch(event1);

            // Assert
            Assert.Equal(2, EventExecuted.Count);
        }

        [Fact]
        public void Dispatch_Domain_Event()
        {
            // Arrange
            var event1 = new TestEvent();
            var event2 = new TestEvent();

            // Act
            this.eventDispatcher.Dispatch(event1);
            this.eventDispatcher.Dispatch(event2);

            // Assert
            Assert.Equal(2, EventExecuted.Count);
        }

        [Fact]
        public void EventBus_raise_events_correctly()
        {
            // Arrange
            var @event = new TestEvent();

            // Act
            this.eventBus.Raise(@event);

            // Assert
            Assert.Equal(2, EventExecuted.Count);
        }

        [Fact]
        public void EventPublisher_publish_domain_events_correctly()
        {
            // Arrange
            // this.PopulateEventsQueue();
            var isAnyDomainEvent = typeof(EventsQueue).GetMethod("IsAnyDomainEvent", BindingFlags.NonPublic | BindingFlags.Static);

            // Act
            this.eventPublisher.PublishEvents();

            // Assert
            Assert.False((bool)isAnyDomainEvent.Invoke(null, null));
            Assert.Equal(13, EventExecuted.Count);
        }
      

        private class TestSerializeService : ISerializeService
        {
  
            public string Serialize(object @object)
            {
                return JsonConvert.SerializeObject(@object);
            }

            public T Deserialize<T>(string @object)
            {
                return JsonConvert.DeserializeObject<T>(@object);
            }

            public object Deserialize(string toDeserialize, Type type)
            {
                return JsonConvert.DeserializeObject(toDeserialize, type);
            }
        }

        private class TestEventHandler1 : EventHandlerBase, IEventHandler<TestEvent>
        {
            public void Handle(TestEvent @event)
            {
                EventsTests.EventExecuted.Add("Test Event Executed - Handler 1");
            }
        }

        private class TestEventHandlerWithThrowEx : EventHandlerBase, IEventHandler<TestEvent>
        {
            public static bool ThrowedException { get; set; }

            public void Handle(TestEvent @event)
            {
                if (ThrowedException == false)
                {
                    ThrowedException = true;
                    throw new Exception("Some failure");
                }

                EventsTests.EventExecuted.Add("Test Event Executed (internal domain) - Handler 3");
                ThrowedException = false;
            }
        }

        private class TestEvent : IEvent
        {
        }
    }
}