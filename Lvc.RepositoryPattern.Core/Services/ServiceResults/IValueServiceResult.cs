namespace Lvc.BackendPatterns.Core.Services.ServiceResults
{
    public interface IValueServiceResult<TValue> : IServiceResult
    {
        TValue Value { get; }
    }
}