namespace Lvc.Performance.Core.Algorithms.HashCodes
{
	public interface IHashCodeProvider
	{
		int Prime1 { get; }
		int Prime2 { get; }

		/// <summary>
		/// Get the hash code of obj.
		/// </summary>
		/// <param name="obj">The object to get the hash code.</param>
		/// <returns>The hash code.</returns>
		int GetHashCode(object obj);

		/// <summary>
		/// Get a hash code based on some fields values.
		/// </summary>
		/// <param name="fieldsValues">The fields values.</param>
		/// <returns>The hash code.</returns>
		int GetHashCode(params object[] fieldsValues);
	}
}