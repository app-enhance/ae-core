namespace AE.Events.Collect
{
    using Core.DI;

    public interface IEventsPublisher : IDependency
    {
        void PublishEvents();
    }
}