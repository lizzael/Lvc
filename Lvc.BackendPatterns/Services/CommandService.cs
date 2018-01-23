using Lvc.BackendPatterns.Core.Services;
using Lvc.BackendPatterns.Core.Services.ServiceResults;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Transactions;

namespace Lvc.BackendPatterns.Services
{
    public abstract class CommandService : BaseService, ICommandService
    {
        private DbContext DbContext { get; }

        public CommandService(
            ILogginService loggingService,
            DbContext dbContext
        ) : base(loggingService) =>
            DbContext = dbContext;
        // Todo: Disable LazyLoading...
        // dbContext.Configuration.LazyLoadingEnabled = false;

        public IEmptyServiceResult Transaction(
            params Action[] actions
        ) =>
            Try(() =>
            {
                using (var transactionScope = new TransactionScope())
                {
                    foreach (var action in actions)
                    {
                        action();

                        DbContext.SaveChanges();
                    }

                    transactionScope.Complete();
                }
            });

        public async Task<IEmptyServiceResult> TransactionAsync(
            params Action[] actions
        ) =>
            await TryAsync(async () => 
            {
                using (var transactionScope = new TransactionScope())
                {
                    foreach (var action in actions)
                    {
                        action();

                        await DbContext
                            .SaveChangesAsync()
                            .ConfigureAwait(false);
                    }

                    transactionScope.Complete();
                }
            }).ConfigureAwait(false);
    }
}
