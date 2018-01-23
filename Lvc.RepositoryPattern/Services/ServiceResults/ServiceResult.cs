using Lvc.BackendPatterns.Core.Services.ServiceResults;
using System;
using System.Linq;

namespace Lvc.BackendPatterns.Services.ServiceResults
{
    public abstract class ServiceResult : IServiceResult
    {
        public bool Succeded =>
            !Exceptions.Any();

        public Exception[] Exceptions { get; set; }

        protected ServiceResult(params Exception[] exceptions) =>
            Exceptions = exceptions ?? new Exception[0];
    }
}