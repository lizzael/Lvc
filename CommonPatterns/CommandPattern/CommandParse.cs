using System.Collections.Generic;
using System.Linq;
using CommonPatterns.Core.CommandPattern;
using CommonPatterns.Core.CommandPattern.Models;

namespace CommonPatterns.CommandPattern
{
	/// <summary>
	/// To be used with NullCommand (Null Object Pattern).
	/// </summary>
	/// <typeparam name="TCommandModel"></typeparam>
	public class CommandParse<TCommandModel>
		: ICommandParser<TCommandModel>
		where TCommandModel : CommandModel
	{
		public IEnumerable<ICommandFactory<TCommandModel>> CommandFactories { get; }

		public CommandParse(IEnumerable<ICommandFactory<TCommandModel>> commandFactories)
		{
			CommandFactories = commandFactories;
		}

		public ICommand<TCommandModel> ParseCommand(TCommandModel commandModel)
		{
			var commandFactory = CommandFactories
				.FirstOrDefault(f => f.CommandName == commandModel.CommandName);

			return commandFactory.BuildCommand(commandModel);
		}
	}
}
