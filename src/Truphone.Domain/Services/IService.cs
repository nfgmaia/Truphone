using System.Collections.Generic;
using System.Threading.Tasks;
using Truphone.Domain.Entities;

namespace Truphone.Domain.Services
{
    public interface IService<T> where T : BaseEntity
    {
        Task<T> Add(T entity);
        Task Delete(long id);
        Task<T> Get(long id);
        Task<IReadOnlyCollection<T>> GetAll();
        Task<T> Update(long id, T entity);
        Task<bool> Exists(long id);
    }
}
