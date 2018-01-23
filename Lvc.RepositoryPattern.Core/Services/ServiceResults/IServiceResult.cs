using System;

namespace Lvc.BackendPatterns.Core.Services.ServiceResults
{
    public interface IServiceResult
    {
        Exception[] Exceptions { get; set; }
        bool Succeded { get; }
    }
}