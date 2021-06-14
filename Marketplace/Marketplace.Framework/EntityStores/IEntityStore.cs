using System.Threading.Tasks;
using Marketplace.Framework.Helpers;

namespace Marketplace.Framework.EntityStores
{
    public interface IEntityStore
    {
        Task<T> LoadAsync<T>(string entityId) where T : Entity;
        
        Task SaveAsync<T>(string entityId, T entity) where T : Entity;

        Task<bool> ExistsAsync<T>(string entityId) where T : Entity;
    }
}