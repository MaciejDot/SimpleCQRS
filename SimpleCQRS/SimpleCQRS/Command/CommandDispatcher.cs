using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCQRS.Command
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDictionary<string, Type> _commandsAndHandlers;
        public CommandDispatcher(IServiceProvider serviceProvider, IDictionary<string, Type> commandsCollection)
        {
            _serviceProvider = serviceProvider;
            _commandsAndHandlers = commandsCollection;
        }

        public Task Dispatch<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : ICommand
        {
            var handler = _serviceProvider.GetService(_commandsAndHandlers[command.GetType().FullName]) as dynamic;
            return (Task) handler.Handle(command, cancellationToken);
        }
    }
}
