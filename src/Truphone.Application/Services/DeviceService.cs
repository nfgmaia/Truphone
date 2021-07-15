using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Truphone.Domain.Adapters;
using Truphone.Domain.Entities;
using Truphone.Domain.Services;

namespace Truphone.Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IUnitOfWork uow;

        public DeviceService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<Device> Add(Device entity)
        {
            var now = DateTime.Now;
            var newEntity = entity with { DateCreated = now, DateUpdated = now };

            var id = await uow.DeviceRepository.Add(newEntity);
            if (id == default) throw new InvalidOperationException("Could not add.");

            return await uow.DeviceRepository.Get(id);
        }

        public async Task Delete(long id)
        {
            var entity = await uow.DeviceRepository.Get(id);

            if (!await uow.DeviceRepository.Delete(entity))
                throw new InvalidOperationException("Could not delete.");
        }

        public async Task<Device> Get(long id)
        {
            return await uow.DeviceRepository.Get(id);
        }

        public async Task<IReadOnlyCollection<Device>> GetAll()
        {
            return await uow.DeviceRepository.GetAll() ?? new List<Device>();
        }

        public async Task<Device> Update(long id, Device entity)
        {
            var e = await uow.DeviceRepository.Get(id);

            var updated = entity with 
            { 
                Id = id, 
                DateCreated = e.DateCreated, 
                DateUpdated = DateTime.Now 
            };

            return await uow.DeviceRepository.Update(updated) 
                ? updated : throw new InvalidOperationException("Could not update.");
        }

        public async Task<IReadOnlyCollection<Device>> Search(string brand)
        {
            return await uow.DeviceRepository.Query(brand) ?? new List<Device>();
        }

        public async Task<bool> Exists(long id)
        {
            return await uow.DeviceRepository.Exists(id);
        }
    }
}
