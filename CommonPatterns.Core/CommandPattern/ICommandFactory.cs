using CommonPatterns.Core.CommandPattern.Models;

namespace CommonPatterns.Core.CommandPattern
{
	/// <summary>
	/// To be used with NullCommand (Null Object Pattern).
	/// </summary>
	/// <typeparam name="TCommandModel"></typeparam>
	public interface ICommandFactory<TCommandModel>
		where TCommandModel : CommandModel
	{
		string CommandName { get; }

		ICommand<TCommandModel> BuildCommand(TCommandModel commandModel);
	}
}
