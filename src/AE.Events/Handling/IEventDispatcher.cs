namespace AE.Events.Handling
{
    using Core.DI;

    public interface IEventDispatcher : IScopedDependency
    {
        void Dispatch<T>(T @event) where T : IEvent;
    }
}