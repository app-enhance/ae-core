namespace AE.Events.Distribute
{
    using Extensions.DependencyInjection;

    public interface IEventBus : ISingletonDependency
    {
        void Raise<T>(T @event) where T : IEvent;
    }
}