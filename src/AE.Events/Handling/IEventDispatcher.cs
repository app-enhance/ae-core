namespace AE.Events.Handling
{
    using Core.DI;

    public interface IEventDispatcher : IDependency
    {
        void Dispatch<T>(T @event) where T : IEvent;
    }
}