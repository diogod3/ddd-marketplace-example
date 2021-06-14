using System;

namespace Marketplace.Domain.Exceptions
{
    public class EventNotSupportedException : Exception
    {
        public EventNotSupportedException(object @event) :
            base($"Event {@event.GetType().Name} not supported")
        {
        }
    }
}