using Lvc.BackendPatterns.Core.Services.ServiceResults;
using System;

namespace Lvc.BackendPatterns.Services.ServiceResults
{
    public class EmptyServiceResult : ServiceResult, IEmptyServiceResult
    {
        public EmptyServiceResult(params Exception[] exceptions)
            : base(exceptions) { }
    }
}
