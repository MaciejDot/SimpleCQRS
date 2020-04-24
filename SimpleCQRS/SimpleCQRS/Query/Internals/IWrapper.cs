using SimpleCQRS.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCQRSDotCore.Query.Internals
{
    internal interface IWrapper<TResponse>
    {
        public Task<TResponse> Handle(object handler, IQuery<TResponse> query, CancellationToken cancellationToken);
    }
}
