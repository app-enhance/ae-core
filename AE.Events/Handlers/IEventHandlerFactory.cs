namespace AE.Events.Handlers
{
    using System.Collections.Generic;

    using AE.Core.DI;

    public interface IEventHandlerFactory : IDependency
    {
        IEnumerable<IEventHandler<T>> SearcHandlers<T>() where T : IEvent;

        IEnumerable<IEventHandler> GetAllEventHandlers();
    }
}