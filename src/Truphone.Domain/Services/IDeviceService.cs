using System.Collections.Generic;
using System.Threading.Tasks;
using Truphone.Domain.Entities;

namespace Truphone.Domain.Services
{
    public interface IDeviceService : IService<Device>
    {
        Task<IReadOnlyCollection<Device>> Search(string brand);
    }
}
