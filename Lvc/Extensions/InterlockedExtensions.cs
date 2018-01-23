using System;
using System.Threading;

namespace Lvc.Extensions
{
    public delegate int Morpher<TResult, TArgument>(int startValue, TArgument argument, out TResult morphResult);

	public delegate TResult AtomicAlgorithm<TArgument, TResult>(TResult startValue, TArgument argument)
		where TResult : class;

	public static class InterlockedExtensions
	{

		/// <summary>
		/// Interlocked Any Operation Pattern
		/// </summary>
		/// <param name="target"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static int InterlockMax(ref int target, int value)
		{
			int targetVal = target, startVal, desiredVal;

			// Don't access target in the loop in an attempt to 
			// change it because another thread may be touching it
			do {
				// Record this iteration starting value
				startVal = targetVal;

				//Calculate desired value in terms of startVal and value
				desiredVal =
					Math.Max(startVal, value); // This could be any complex algorithm!!!

				/* Note: The thread could be pre-empted here */

				// If target's content changed behind our thread's back; do not change target
				// else, set target to desiredVal; targetVal set to what CompareExchange saw
				targetVal = 
					Interlocked.CompareExchange(ref target, desiredVal, startVal); // There is a generic version!!!

				// If CompareExchange detected changed value during this iteration, try again
			} while (startVal != targetVal);

			// Return what this thread did to target
			return desiredVal;
		}

		public static TResult Morph<TResult, TArgument>(
			ref int target, TArgument argument, Morpher<TResult, TArgument> morpher) 
		{
			TResult morphResult;
			int currentVal = target, startVal, desiredVal;
			
			do {
				startVal = currentVal;
				desiredVal = morpher(startVal, argument, out morphResult);
				currentVal = 
					Interlocked.CompareExchange(ref target, desiredVal, startVal); // Generic overload! Can be generalized!!!
			} while (startVal != currentVal);

			return morphResult;
		}

		public static TResult InterlockedAtomicAlgorithm<TArgument, TResult>(
			ref TResult target, TArgument argument, 
			AtomicAlgorithm<TArgument, TResult> atomicAlgorithm)
			where TResult : class
		{
			TResult currentVal = target, startVal, result;

			do
			{
				startVal = currentVal;
				result = atomicAlgorithm(startVal, argument);
				currentVal =
					Interlocked.CompareExchange(ref target, result, startVal); // Generic overload! Can be generalized!!!
			} while (startVal != currentVal);

			return result;
		}
	}
}
