using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCQRSDotCore.Query
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDictionary<string, Type> _handlerTypes;
        public QueryProcessor(IServiceProvider serviceProvider, IDictionary<string, Type> queriesCollection)
        {
            _serviceProvider = serviceProvider;
            _handlerTypes = queriesCollection;
        }

        public Task<TResponse> Process<TResponse>(IQuery<TResponse> command, CancellationToken cancellationToken)
        {
            var handler = (IQueryHandler<IQuery<TResponse>, TResponse>) _serviceProvider.GetService(_handlerTypes[command.GetType().FullName]);
            return handler.Handle(command, cancellationToken);
        }
    }
}
