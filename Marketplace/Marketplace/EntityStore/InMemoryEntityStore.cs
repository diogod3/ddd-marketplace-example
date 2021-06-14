using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marketplace.Framework.EntityStores;
using Marketplace.Framework.Helpers;

namespace Marketplace.EntityStore
{
    public class InMemoryEntityStore : IEntityStore
    {
        private static readonly Dictionary<string, Entity> Entities = new();

       
        public Task<bool> ExistsAsync<T>(string entityId) where T : Entity
        {
            if (string.IsNullOrWhiteSpace(entityId))
            {
                throw new ArgumentException($"'{nameof(entityId)}' cannot be null or whitespace.", nameof(entityId));
            }

            return Task.FromResult(Entities.ContainsKey(entityId));
        }

        public Task<T> LoadAsync<T>(string entityId) where T : Entity
        {
            if (string.IsNullOrWhiteSpace(entityId))
            {
                throw new ArgumentException($"'{nameof(entityId)}' cannot be null or whitespace.", nameof(entityId));
            }

            T entity = null;
            if (Entities.TryGetValue(entityId, out var savedEntity))
            {
                entity = savedEntity as T;
            }

            return Task.FromResult(entity);
        }

        public Task SaveAsync<T>(string entityId, T entity) where T : Entity
        {
            if (string.IsNullOrWhiteSpace(entityId))
            {
                throw new ArgumentException($"'{nameof(entityId)}' cannot be null or whitespace.", nameof(entityId));
            }

            Entities[entityId] = entity ?? throw new ArgumentNullException(nameof(entity));

            return Task.CompletedTask;
        }
    }
}
