using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCQRSDotCore.Command
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
            var handler = (ICommandHandler<TCommand>) _serviceProvider.GetService(_commandsAndHandlers[command.GetType().FullName]);
            return handler.Handle(command, cancellationToken);
        }
    }
}
