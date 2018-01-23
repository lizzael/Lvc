using System;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        Task SaveAsync();
    }
}