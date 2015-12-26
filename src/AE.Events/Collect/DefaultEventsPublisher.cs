namespace AE.Events.Collect
{
    using AE.Events.Distribute;

    public class DefaultEventsPublisher : IEventsPublisher
    {
        protected readonly IEventBus eventBus;

        public DefaultEventsPublisher(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public virtual void PublishEvents()
        {
            while (EventsQueue.IsAnyEvent())
            {
                var @event = EventsQueue.GetNextEvent();
                this.eventBus.Raise(@event);
            }
        }
    }
}