using Marketplace.Framework.Helpers;
using System.Collections.Generic;
using System.Linq;
using Marketplace.Framework.Events;

namespace Marketplace.Framework.Aggregates
{
    public abstract class AggregateRoot<TId> : IInternalEventHandler
        where TId : Value<TId>
    {
        private readonly List<object> _changes = new();

        public TId Id { get; protected set; }

        protected abstract void When(object @event);

        protected abstract void EnsureValidState();

        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _changes.Add(@event);
        }
        
        public IEnumerable<object> GetChanges()
        {
            return _changes.AsEnumerable();
        }

        public void ClearChanges()
        {
            _changes.Clear();
        }

        protected void ApplyToEntity(IInternalEventHandler entity, object @event)
        {
            entity?.Handle(@event);
        }

        public void Handle(object @event)
        {
           When(@event);
        }
    }
}