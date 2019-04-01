using Lvc.BackendPatterns.Core.Services;
using Lvc.BackendPatterns.Core.Services.ServiceResults;
using System;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns.Services
{
    public abstract class CommandService : BaseService, ICommandService
    {
        public ICommandSaver CommandSaver { get; }

        public CommandService(
            ILogginService loggingService,
            ICommandSaver commandSaver
        ) : base(loggingService) =>
            CommandSaver = commandSaver;


        // Todo: Disable LazyLoading...
        // dbContext.Configuration.LazyLoadingEnabled = false;

        public IEmptyServiceResult Transaction(
            params Action[] actions
        ) =>
            Try(() =>
            {
                foreach (var action in actions)
                {
                    action();
                }

                CommandSaver.Save();
            });

        public async Task<IEmptyServiceResult> TransactionAsync(
            params Func<Task>[] asyncActions
        ) =>
            await TryAsync(async () => 
            {
                foreach (var asyncAction in asyncActions)
                {
                    await asyncAction();
                }

                await CommandSaver
                    .SaveAsync()
                    .ConfigureAwait(false);
            }).ConfigureAwait(false);
    }
}
