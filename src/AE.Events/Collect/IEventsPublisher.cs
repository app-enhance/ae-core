namespace AE.Events.Collect
{
    using AE.Core.DI;

    public interface IEventsPublisher : IDependency
    {
        void PublishEvents();
    }
}