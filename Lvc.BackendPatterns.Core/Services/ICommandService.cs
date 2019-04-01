using Lvc.BackendPatterns.Core.Services.ServiceResults;
using System;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns.Core.Services
{
    public interface ICommandService
    {
        IEmptyServiceResult Transaction(
            params Action[] actions);
        Task<IEmptyServiceResult> TransactionAsync(
            params Func<Task>[] asyncActions);
    }
}