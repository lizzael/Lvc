using Lvc.BackendPatterns.Core.Services.ServiceResults;
using System;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns.Core.Services
{
    public interface IQueryService
    {
        IValueServiceResult<TValue> GetResult<TValue>(
            Func<TValue> func);
        Task<IValueServiceResult<TValue>> GetResultAsync<TValue>(
            Func<Task<TValue>> funcAsync);
    }
}
