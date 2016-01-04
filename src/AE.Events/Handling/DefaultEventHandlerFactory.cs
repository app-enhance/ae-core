namespace AE.Events.Handling
{
    using System;
    using System.Collections.Generic;

    using Core.DI;

    public class DefaultEventHandlerFactory : IEventHandlerFactory
    {
        protected readonly IServiceProvider _serviceProvider;

        public DefaultEventHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual IEnumerable<IEventHandler<T>> SearcHandlers<T>() where T : IEvent
        {
            var eventHandlers = new List<IEventHandler<T>>(_serviceProvider.Resolve<IEnumerable<IEventHandler<T>>>());
            return eventHandlers;
        }

        public virtual IEnumerable<IEventHandler> GetAllEventHandlers()
        {
            var eventHandlers = new List<IEventHandler>(_serviceProvider.Resolve<IEnumerable<IEventHandler>>());
            return eventHandlers;
        }
    }
}