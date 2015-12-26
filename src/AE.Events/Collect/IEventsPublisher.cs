namespace AE.Events.Services
{
    using AE.Core.DI;

    public interface IEventsPublisher : IDependency
    {
        void PublishEvents();
    }
}