using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCQRS.Command
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReadOnlyDictionary<string, Type> _commandsAndHandlers;
        public CommandDispatcher(IServiceProvider serviceProvider, IReadOnlyDictionary<string, Type> commandsCollection)
        {
            _serviceProvider = serviceProvider;
            _commandsAndHandlers = commandsCollection;
        }

        public Task Dispatch<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
        {
            var handler = _serviceProvider.GetService(_commandsAndHandlers[command.GetType().FullName]);
            MethodInfo magicMethod = handler.GetType().GetMethod("Handle");
            return (Task)magicMethod.Invoke(handler, new object[] { command, cancellationToken });
        }
    }
}
