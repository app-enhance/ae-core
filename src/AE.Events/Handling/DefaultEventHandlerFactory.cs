namespace AE.Events.Handling
{
    using System.Collections.Generic;

    using Core.DI;

    public class DefaultEventHandlerFactory : IEventHandlerFactory
    {
        protected readonly IComponentResolver _componentResolver;

        public DefaultEventHandlerFactory(IComponentResolver componentResolver)
        {
            _componentResolver = componentResolver;
        }

        public virtual IEnumerable<IEventHandler<T>> SearcHandlers<T>() where T : IEvent
        {
            var eventHandlers = new List<IEventHandler<T>>(_componentResolver.Resolve<IEnumerable<IEventHandler<T>>>());
            return eventHandlers;
        }

        public virtual IEnumerable<IEventHandler> GetAllEventHandlers()
        {
            var eventHandlers = new List<IEventHandler>(_componentResolver.Resolve<IEnumerable<IEventHandler>>());
            return eventHandlers;
        }
    }
}