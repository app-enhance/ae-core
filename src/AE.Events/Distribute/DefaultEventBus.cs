namespace AE.Events.Distribute
{
    using Handling;

    public class DefaultEventBus : IEventBus
    {
        protected readonly IEventDispatcher _eventDispatcher;

        public DefaultEventBus(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        public virtual void Raise<T>(T @event) where T : IEvent
        {
            _eventDispatcher.Dispatch(@event);
        }
    }
}