namespace AE.Events.Handling
{
    using Core.DI;

    public interface IEventHandler : IScopedDependency
    {
    }

    public interface IEventHandler<in T> : IEventHandler where T : IEvent
    {
        void Handle(T @event);
    }
}