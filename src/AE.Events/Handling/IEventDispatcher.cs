namespace AE.Events.Handling
{
    using Extensions.DependencyInjection;

    public interface IEventDispatcher : IScopedDependency
    {
        void Dispatch<T>(T @event) where T : IEvent;
    }
}