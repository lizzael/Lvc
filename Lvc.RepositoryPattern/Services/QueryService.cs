using Lvc.BackendPatterns.Core.Services;
using Lvc.BackendPatterns.Core.Services.ServiceResults;
using System;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns.Services
{
    public abstract class QueryService : BaseService, IQueryService
    {
        public QueryService(ILogginService loggingService)
            : base(loggingService) { }

        public IValueServiceResult<TValue> GetResult<TValue>(
            Func<TValue> func
        ) =>
            Try(() => 
                func()
            );

        public async Task<IValueServiceResult<TValue>> GetResultAsync<TValue>(
            Func<Task<TValue>> funcAsync
        ) =>
            await TryAsync(async () =>
                await funcAsync()
                    .ConfigureAwait(false)
            ).ConfigureAwait(false);
    }
}
