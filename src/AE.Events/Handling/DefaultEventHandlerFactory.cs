namespace AE.Events.Handling
{
    using System.Collections.Generic;

    using AE.Core.DI;

    public class DefaultEventHandlerFactory : IEventHandlerFactory
    {
        protected readonly IComponentResolver componentResolver;

        public DefaultEventHandlerFactory(IComponentResolver componentResolver)
        {
            this.componentResolver = componentResolver;
        }

        public virtual IEnumerable<IEventHandler<T>> SearcHandlers<T>() where T : IEvent
        {
            var eventHandlers = new List<IEventHandler<T>>(this.componentResolver.Resolve<IEnumerable<IEventHandler<T>>>());
            return eventHandlers;
        }

        public virtual IEnumerable<IEventHandler> GetAllEventHandlers()
        {
            var eventHandlers = new List<IEventHandler>(this.componentResolver.Resolve<IEnumerable<IEventHandler>>());
            return eventHandlers;
        }
    }
}