namespace AE.Events.Handling
{
    using System;
    using System.Linq;

    using Core.Logging;

    public class DefaultEventDispatcher : IEventDispatcher
    {
        private readonly IEventHandlerFactory _eventHandlerFactory;

        public DefaultEventDispatcher(IEventHandlerFactory eventHandlerFactory)
        {
            _eventHandlerFactory = eventHandlerFactory;
            Logger = NullLogger.Instance;
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
                        Logger.Error($"Execution of event throw exception. Event: {typeof(T).Name}", e);
                    }
                };

            Handle(@event, exceptionSwallowing);
        }

        protected virtual void Handle<T>(T @event, Action<Action> handleWrapper = null) where T : IEvent
        {
            if (handleWrapper == null)
            {
                handleWrapper = handle => handle();
            }

            var handlers = _eventHandlerFactory.SearcHandlers<T>().ToList();
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