using System.Collections.Generic;
using System.Linq;

namespace Marketplace.Framework.Helpers
{
    public abstract class Entity
    {
        #region Fields

        private readonly List<object> _events = new();

        #endregion

        #region Public Methods

        protected abstract void When(object @event);

        protected abstract void EnsureValidState();

        protected void Apply(object @event)
        {
            When(@event);
            EnsureValidState();
            _events.Add(@event);
        }

        public IEnumerable<object> GetChanges()
        {
            return _events.AsEnumerable();
        }

        public void ClearChanges()
        {
            _events.Clear();
        }

        #endregion
    }
}