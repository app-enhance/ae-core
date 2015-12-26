namespace AE.Events.Distribute
{
    using AE.Events.Handling;

    public class DefaultEventBus : IEventBus
    {
        protected readonly IEventDispatcher eventDispatcher;

        public DefaultEventBus(IEventDispatcher eventDispatcher)
        {
            this.eventDispatcher = eventDispatcher;
        }

        public virtual void Raise<T>(T @event) where T : IEvent
        {
            this.eventDispatcher.Dispatch(@event);
        }
    }
}