using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCQRS.Query
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IReadOnlyDictionary<string, Type> _handlerTypes;
        public QueryProcessor(IServiceProvider serviceProvider, IReadOnlyDictionary<string, Type> queriesCollection)
        {
            _serviceProvider = serviceProvider;
            _handlerTypes = queriesCollection;
        }

        public Task<TResponse> Process<TResponse>(IQuery<TResponse> command, CancellationToken cancellationToken)
        {
            var handler = _serviceProvider.GetService(_handlerTypes[command.GetType().FullName]);
            MethodInfo magicMethod = handler.GetType().GetMethod("Handle");
            return (Task<TResponse>)magicMethod.Invoke(handler, new object[]{ command, cancellationToken});
        }
    }
}
