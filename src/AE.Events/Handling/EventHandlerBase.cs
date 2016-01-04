namespace AE.Events.Handling
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using Core.Logging;

    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     This class allow to resolve events arriving from outsite of application.
    /// </summary>
    /// <remarks>
    ///     It is helpfull when events have been persisted in storage and they lost concrete type.
    ///     Parameter of handle method should be named 'event' otherwise it gets first parameter.
    /// </remarks>
    public abstract class EventHandlerBase : IEventHandler<IEvent>
    {
        protected static readonly Type EventType = typeof(IEvent);

        protected static readonly ConcurrentDictionary<Type, ConcurrentDictionary<Type, MethodInfo>> HandlersMapping =
            new ConcurrentDictionary<Type, ConcurrentDictionary<Type, MethodInfo>>();

        public EventHandlerBase()
        {
            var type = GetType();
            if (HandlersMapping.ContainsKey(type) == false)
            {
                var handlerInterfaces = type.GetInterfaces().Where(IsFitEventHandlerType);

                var handlingMethods = handlerInterfaces.Select(SelectHandlingMethods);
                HandlersMapping.TryAdd(type, new ConcurrentDictionary<Type, MethodInfo>(handlingMethods));
            }

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public virtual void Handle(IEvent @event)
        {
            if (@event != null)
            {
                var eventType = @event.GetType();

                var handlingMethod = RetrieveHandler(eventType);
                if (handlingMethod != null)
                {
                    handlingMethod.Invoke(this, new object[] { @event });
                    Logger.LogDebug($"Handled event: {eventType.Name}", @event);
                }
            }
        }

        protected static bool IsFitEventHandlerType(Type type)
        {
            if ((type.GetTypeInfo().IsGenericType == false) || (type == typeof(IEventHandler<IEvent>)))
            {
                return false;
            }

            var genericDefinition = type.GetGenericTypeDefinition();
            return genericDefinition == typeof(IEventHandler<>);
        }

        protected static KeyValuePair<Type, MethodInfo> SelectHandlingMethods(Type @interface)
        {
            var method = @interface.GetMethod("Handle");
            if (method == null)
            {
                throw new ArgumentException("Incorrect interface type", "@interface");
            }

            return new KeyValuePair<Type, MethodInfo>(GetParameterType(method), method);
        }

        protected static Type GetParameterType(MethodInfo method)
        {
            var parameters = method.GetParameters();
            var eventParameter = parameters.SingleOrDefault(x => x.Name == "event") ?? parameters.FirstOrDefault();

            return eventParameter?.ParameterType;
        }

        protected virtual MethodInfo RetrieveHandler(Type eventType)
        {
            if ((eventType == null) || (EventType.IsAssignableFrom(eventType) == false))
            {
                return null;
            }

            ConcurrentDictionary<Type, MethodInfo> handlers;
            if (HandlersMapping.TryGetValue(GetType(), out handlers) == false)
            {
                return null;
            }

            MethodInfo handler;
            if (handlers.TryGetValue(eventType, out handler) == false)
            {
                return null;
            }

            return handler;
        }
    }
}