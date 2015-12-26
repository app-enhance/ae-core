namespace AE.Events.Handling
{
    using AE.Core.DI;

    public interface IEventHandler : IDependency
    {
    }

    public interface IEventHandler<in T> : IEventHandler where T : IEvent
    {
        void Handle(T @event);
    }
}