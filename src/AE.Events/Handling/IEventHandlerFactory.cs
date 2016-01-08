namespace AE.Events.Handling
{
    using System.Collections.Generic;

    using Extensions.DependencyInjection;

    public interface IEventHandlerFactory : IScopedDependency
    {
        IEnumerable<IEventHandler<T>> SearcHandlers<T>() where T : IEvent;

        IEnumerable<IEventHandler> GetAllEventHandlers();
    }
}