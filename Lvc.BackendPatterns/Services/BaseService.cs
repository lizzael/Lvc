using Lvc.BackendPatterns.Core.Services;
using Lvc.BackendPatterns.Core.Services.ServiceResults;
using Lvc.BackendPatterns.Services.ServiceResults;
using System;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns.Services
{
    public class BaseService
    {
        private ILogginService LoggingService { get; }

        public BaseService(ILogginService loggingService)=>
            LoggingService = loggingService;

        public IEmptyServiceResult Try(Action action)
        {
            try
            {
                action();

                return new EmptyServiceResult();
            }
            catch(Exception ex)
            {
                LoggingService.Log(ex);

                return new EmptyServiceResult(ex);
            }
        }

        public async Task<IEmptyServiceResult> TryAsync(Func<Task> funcAsync)
        {
            try
            {
                await funcAsync()
                    .ConfigureAwait(false);

                return new EmptyServiceResult();
            }
            catch (Exception ex)
            {
                await LoggingService
                    .LogAsync(ex)
                    .ConfigureAwait(false);

                return new EmptyServiceResult(ex);
            }
        }

        public IValueServiceResult<TValue> Try<TValue>(Func<TValue> func)
        {
            try
            {
                var result = func();

                return new ValueServiceResult<TValue>(result);
            }
            catch (Exception ex)
            {
                LoggingService.Log(ex);

                return new ValueServiceResult<TValue>(ex);
            }
        }

        public async Task<IValueServiceResult<TValue>> TryAsync<TValue>(
            Func<Task<TValue>> funcAsync)
        {
            try
            {
                var result = await funcAsync()
                    .ConfigureAwait(false);

                return new ValueServiceResult<TValue>(result);
            }
            catch (Exception ex)
            {
                await LoggingService
                    .LogAsync(ex)
                    .ConfigureAwait(false);

                return new ValueServiceResult<TValue>(ex);
            }
        }
    }
}
