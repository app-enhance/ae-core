namespace AE.Events.Handlers
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Reflection;


    /// <summary>
    /// This class allow to resolve events arriving from outsite of application.
    /// </summary>
    /// <remarks>
    /// It is helpfull when events have been persisted in storage and they lost concrete type.
    /// Parameter of handle method should be named 'event' otherwise it gets first parameter.
    /// Trick: Handler can have methods without parameters. They will run for all calls (even if event == null)
    /// </remarks>
    public abstract class EventHandlerBase : IEventHandler<IEvent>
    {
        protected static ConcurrentDictionary<Type, MethodInfo> HandlersMapping;

        protected static ConcurrentBag<MethodInfo> EveryCallHandlers; 

        public EventHandlerBase()
        {
            var handlerInterfaces = this.GetType().GetInterfaces().Where(x => typeof(IEventHandler).IsAssignableFrom(x));
            var handlingMethods = handlerInterfaces.Select(SelectHandlingMethods).Where(x => x != null);

            // Split methods for handling concrete types and for every call
            var handlersLookup = handlingMethods.ToLookup(x => x.ParameterType == null);

            HandlersMapping = new ConcurrentDictionary<Type, MethodInfo>(handlersLookup[false].ToDictionary(k => k.ParameterType, v => v.Method));
            EveryCallHandlers = new ConcurrentBag<MethodInfo>(handlersLookup[true].Select(x => x.Method));
        }

        public virtual void Handle(IEvent @event)
        {
            foreach (var handlingMethod in EveryCallHandlers)
            {
                handlingMethod.Invoke(this, null);
            }

            if (@event != null)
            {
                var eventType = @event.GetType();

                MethodInfo handlingMethod;
                if (HandlersMapping.TryGetValue(eventType, out handlingMethod))
                {
                    handlingMethod.Invoke(this, new object[] { @event });
                }
            }
        }

        protected static MethodMap SelectHandlingMethods(Type @interface)
        {
            var method = @interface.GetMethod("Handle");
            if (method == null)
            {
                return null;
            }

            return new MethodMap { Method = method, ParameterType = GetParameterType(method) };
        }

        protected static Type GetParameterType(MethodInfo method)
        {
            var parameters = method.GetParameters();
            var eventParameter = parameters.SingleOrDefault(x => x.Name == "event") ?? parameters.FirstOrDefault();
            if (eventParameter == null)
            {
                return null;
            }

            return eventParameter.ParameterType;
        }

        protected class MethodMap
        {
            public Type ParameterType { get; set; }

            public MethodInfo Method { get; set; }
        }
    }
}