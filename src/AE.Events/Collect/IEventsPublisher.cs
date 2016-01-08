namespace AE.Events.Collect
{
    using Core.DI;

    public interface IEventsPublisher : IScopedDependency
    {
        void PublishEvents();
    }
}