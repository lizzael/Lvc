using Lvc.BackendPatterns.Core.Services.ServiceResults;
using System;

namespace Lvc.BackendPatterns.Services.ServiceResults
{
    public class ValueServiceResult<TValue> : ServiceResult, IValueServiceResult<TValue>
    {
        public TValue Value { get; }

        public ValueServiceResult(TValue value) =>
            Value = value;

        public ValueServiceResult(params Exception[] exceptions)
            : base(exceptions) { }
    }
}
