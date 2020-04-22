using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCQRS.Query
{
    public interface IQueryProcessor
    {
        public Task<TResponse> Process<TResponse>(IQuery<TResponse> command, CancellationToken cancellationToken);
    }
}
