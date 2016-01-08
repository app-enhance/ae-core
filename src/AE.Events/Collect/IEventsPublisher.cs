namespace AE.Events.Collect
{
    using Extensions.DependencyInjection;

    public interface IEventsPublisher : IScopedDependency
    {
        void PublishEvents();
    }
}