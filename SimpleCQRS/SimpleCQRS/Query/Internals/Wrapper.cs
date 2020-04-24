using SimpleCQRS.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCQRSDotCore.Query.Internals
{
    internal class Wrapper<TQuery, TResponse> : IWrapper<TResponse> where TQuery : IQuery<TResponse>
    {
        public Task<TResponse> Handle(object handler, IQuery<TResponse> query, CancellationToken cancellationToken)
        {
            return ((IQueryHandler<TQuery, TResponse>)handler).Handle((TQuery)query, cancellationToken);

        }
    }
}
