using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Framework.Helpers
{
    public abstract class Entity
    {
        private readonly List<object> _events = new();

        protected void Raise(object @event) => _events.Add(@event);

        public IEnumerable<object> GetChanges() => _events.AsEnumerable();

        public void ClearChanges() => _events.Clear();
    }
}