using System;
using System.Threading.Tasks;

namespace Lvc.BackendPatterns.Core.Services
{
    /// <summary>
    /// Have to be implemented by the app.
    /// </summary>
    public interface ILogginService
    {
        void Log(Exception ex);
        Task LogAsync(Exception ex);
    }
}
