namespace AE.Events.Collect
{
    using Distribute;

    public class DefaultEventsPublisher : IEventsPublisher
    {
        protected readonly IEventBus _eventBus;

        public DefaultEventsPublisher(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public virtual void PublishEvents()
        {
            while (EventsQueue.IsAnyEvent())
            {
                var @event = EventsQueue.GetNextEvent();
                _eventBus.Raise(@event);
            }
        }
    }
}