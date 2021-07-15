using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Truphone.Domain.Adapters;
using Truphone.Domain.Entities;

namespace Truphone.Database.Repositories
{
    class DeviceRepository : IDeviceRepository
    {
        private DbSession session;

        public DeviceRepository(DbSession session)
        {
            this.session = session;
        }

        public async Task<long> Add(Device entity)
        {
            return await session.Connection.InsertAsync(entity, session.Transaction);
        }

        public async Task<bool> Delete(Device entity)
        {
            return await session.Connection.DeleteAsync(entity, session.Transaction);
        }

        public async Task<Device> Get(long id)
        {
            return await session.Connection.GetAsync<Device>(id, session.Transaction);
        }

        public async Task<IReadOnlyCollection<Device>> GetAll()
        {
            return (await session.Connection.GetAllAsync<Device>(session.Transaction)).ToList();
        }

        public async Task<IReadOnlyCollection<Device>> Query(string brand)
        {
            var parameters = new { Brand = brand };
            var sql = "SELECT * FROM DEVICES WHERE UPPER(BRAND) = UPPER(@Brand)";

            return (await session.Connection.QueryAsync<Device>(sql, parameters, session.Transaction)).ToList();
        }

        public async Task<bool> Update(Device entity)
        {
            return await session.Connection.UpdateAsync(entity, session.Transaction);
        }

        public async Task<bool> Exists(long id)
        {
            var parameters = new { Id = id };
            var sql = "SELECT 1 FROM DEVICES WHERE ID = @Id LIMIT 1";

            return (await session.Connection.QueryAsync<Device>(sql, parameters, session.Transaction)).Any();
        }
    }
}
