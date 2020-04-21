using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCQRSDotCore.Command
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        public Task Handle(TCommand command, CancellationToken cancellationToken);
    }
}
