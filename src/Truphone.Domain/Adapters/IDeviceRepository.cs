using System.Collections.Generic;
using System.Threading.Tasks;
using Truphone.Domain.Entities;

namespace Truphone.Domain.Adapters
{
    public interface IDeviceRepository : IRepository<Device>
    {
        Task<IReadOnlyCollection<Device>> Query(string brand);
    }
}
