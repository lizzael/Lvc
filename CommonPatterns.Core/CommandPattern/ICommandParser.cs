using System.Collections.Generic;
using CommonPatterns.Core.CommandPattern.Models;

namespace CommonPatterns.Core.CommandPattern
{
	public interface ICommandParser<TCommandModel>
		where TCommandModel : CommandModel
	{
		IEnumerable<ICommandFactory<TCommandModel>> CommandFactories { get; }
		ICommand<TCommandModel> ParseCommand(TCommandModel commandModel);
	}
}
