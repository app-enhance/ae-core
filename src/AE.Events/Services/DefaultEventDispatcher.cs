namespace AE.Events.Services
{
    using System;
    using System.Linq;

    using AE.Core.Logging;
    using AE.Events.Handlers;

    public class DefaultEventDispatcher : IEventDispatcher
    {
        protected readonly IEventHandlerFactory eventHandlerFactory;

        public DefaultEventDispatcher(IEventHandlerFactory eventHandlerFactory)
        {
            this.eventHandlerFactory = eventHandlerFactory;
            this.Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public virtual void Dispatch<T>(T @event) where T : IEvent
        {
            Action<Action> exceptionSwallowing = handle =>
                {
                    try
                    {
                        handle();
                    }
                    catch (Exception e)
                    {
                        this.Logger.Error("Execution of event throw exception", e);
                    }
                };

            this.Handle(@event, exceptionSwallowing);
        }

        protected virtual void Handle<T>(T @event, Action<Action> handleWrapper = null) where T : IEvent
        {
            if (handleWrapper == null)
            {
                handleWrapper = handle => handle();
            }

            var handlers = this.eventHandlerFactory.SearcHandlers<T>();
            
            if (handlers.Any())
            {
                foreach (var eventHandler in handlers)
                {
                    handleWrapper(() => eventHandler.Handle(@event));
                }
            }
        }
    }
}