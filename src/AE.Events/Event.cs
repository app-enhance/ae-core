namespace AE.Events
{
    using System;

    public abstract class Event : IEvent
    {
        protected Event()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}