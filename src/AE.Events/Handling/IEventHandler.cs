namespace AE.Events.Handling
{
    using Extensions.DependencyInjection;

    public interface IEventHandler : IScopedDependency
    {
    }

    public interface IEventHandler<in T> : IEventHandler where T : IEvent
    {
        void Handle(T @event);
    }
}