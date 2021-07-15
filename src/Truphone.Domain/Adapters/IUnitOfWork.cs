using System;
using System.Data;

namespace Truphone.Domain.Adapters
{
    public interface IUnitOfWork : IDisposable
    {
        IDeviceRepository DeviceRepository { get; }
        IDbTransaction BeginTransaction();
        void Commit();
        void Rollback();
    }
}
