namespace AE.Events.Services
{
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