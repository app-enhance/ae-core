namespace AE.Events.Distribute
{
    using Core.DI;

    public interface IEventBus : ISingletonDependency
    {
        void Raise<T>(T @event) where T : IEvent;
    }
}