using System.Data;
using Truphone.Domain.Adapters;

namespace Truphone.Database
{
    class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession session;
        private readonly IDeviceRepository deviceRepository;

        public UnitOfWork(DbSession session, IDeviceRepository deviceRepository)
        {
            this.session = session;
            this.deviceRepository = deviceRepository;
        }

        public IDeviceRepository DeviceRepository => deviceRepository;

        public IDbTransaction BeginTransaction()
        {
            return session.Transaction = session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            session.Transaction.Commit();
            Dispose();
        }

        public void Rollback()
        {
            session.Transaction.Rollback();
            Dispose();
        }

        public void Dispose() => session.Transaction?.Dispose();
    }
}
