using System.Collections.Generic;
using System.Threading.Tasks;
using Truphone.Domain.Entities;

namespace Truphone.Domain.Adapters
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<long> Add(T entity);
        Task<bool> Delete(T entity);
        Task<T> Get(long id);
        Task<IReadOnlyCollection<T>> GetAll();
        Task<bool> Update(T entity);
        Task<bool> Exists(long id);
    }
}
