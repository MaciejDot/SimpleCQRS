﻿using System;
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
            var handler = (ICommandHandler<TCommand>)_serviceProvider.GetService(_commandsAndHandlers[typeof(TCommand).FullName]);
            return (Task) handler.Handle(command, cancellationToken);
        }
    }
}
