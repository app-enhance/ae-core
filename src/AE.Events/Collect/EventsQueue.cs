namespace AE.Events.Collect
{
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Threading;

    // TODO: Totaly to refactoring it cannot base on threads
    public static class EventsQueue
    {
        private static readonly ConcurrentDictionary<Thread, ConcurrentQueue<IEvent>> QueuesDictionary =
            new ConcurrentDictionary<Thread, ConcurrentQueue<IEvent>>();

        public static void AddEvent(IEvent @event)
        {
            var currentThread = Thread.CurrentThread;
            var queue = QueuesDictionary.GetOrAdd(currentThread, x => new ConcurrentQueue<IEvent>());
            queue.Enqueue(@event);
        }

        public static void ClearAll()
        {
            foreach (var theread in QueuesDictionary)
            {
                ClearQueue(theread.Value);
            }

            QueuesDictionary.Clear();
        }

        public static void ClearCurrent()
        {
            var currentThread = Thread.CurrentThread;
            ConcurrentQueue<IEvent> queue;
            if (QueuesDictionary.TryGetValue(currentThread, out queue))
            {
                ClearQueue(queue);
            }

            QueuesDictionary.TryRemove(currentThread, out queue);
        }

        internal static bool IsAnyEvent()
        {
            return QueuesDictionary.Any(queue => !queue.Value.IsEmpty);
        }

        internal static IEvent GetNextEvent()
        {
            while (true)
            {
                var firstThread = QueuesDictionary.FirstOrDefault();
                if (firstThread.Key == null)
                {
                    return null;
                }

                var queue = firstThread.Value;

                IEvent @event;
                if (queue.TryDequeue(out @event))
                {
                    return @event;
                }

                QueuesDictionary.TryRemove(firstThread.Key, out queue);
            }
        }

        private static void ClearQueue(ConcurrentQueue<IEvent> queue)
        {
            IEvent domanEvent;
            while (queue.TryDequeue(out domanEvent))
            {
            }
        }
    }
}