namespace AE.Events.Distribute
{
    using AE.Core.DI;

    public interface IEventBus : ISingletonDependency
    {
        void Raise<T>(T @event) where T : IEvent;
    }
}