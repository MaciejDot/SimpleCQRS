using SimpleCQRSDotCore.Query.Internals;
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

        public Task<TResponse> Process<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken)
        {
            var queryType = query.GetType();
            var handler = _serviceProvider.GetService(_handlerTypes[queryType.FullName]);
            var wrapper = (IWrapper<TResponse>)Activator.CreateInstance(typeof(Wrapper<,>).MakeGenericType(query.GetType(), typeof(TResponse)));
            return wrapper.Handle(handler, query, cancellationToken);
        }
    }
   
}
