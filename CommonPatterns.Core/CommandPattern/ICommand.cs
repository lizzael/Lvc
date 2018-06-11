using CommonPatterns.Core.CommandPattern.Models;

namespace CommonPatterns.Core.CommandPattern
{
	public interface ICommand<TCommandModel>
		where TCommandModel : CommandModel
	{
		TCommandModel CommandModel { get; }

		/// <summary>
		/// Check if the data is valid to execute the command.
		/// </summary>
		/// <returns></returns>
		bool IsValid();

		void Execute();

		ICommand<TCommandModel> GetUndoCommand();
	}
}