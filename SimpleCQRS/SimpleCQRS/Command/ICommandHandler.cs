using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCQRS.Command
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        public Task Handle(TCommand command, CancellationToken cancellationToken);
    }
}
