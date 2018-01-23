using System;
using System.Threading;

namespace Lvc.Threading
{
	public static class SingletonExecution
	{
		public static bool Execute(string semaphoreName, Action action)
		{
            // This semaphore can sinchronize across differents processes!
            using (var semaphore = new Semaphore(0, 1, semaphoreName, out bool createdNew))
            {
                if (createdNew)
                    action();

                return createdNew;
            }
        }

		public static bool Execute<TResult>(string semaphoreName, Func<TResult> func, out TResult result)
		{
            // This semaphore can sinchronize across differents processes!
            using (var semaphore = new Semaphore(0, 1, semaphoreName, out bool createdNew))
            {
                result = createdNew
                    ? func()
                    : default(TResult);

                return createdNew;
            }
        }
	}
}