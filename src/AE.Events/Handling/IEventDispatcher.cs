namespace AE.Events.Handling
{
    using AE.Core.DI;

    public interface IEventDispatcher : IDependency
    {
        void Dispatch<T>(T @event) where T : IEvent;
    }
}