using Autofac;

namespace LFM.ApplicationServices
{
    /// <summary>
    /// The query invoker is responsible for read only operations
    /// </summary>
    public class QueryInvoker : IQueryInvoker
    {
        private readonly ILifetimeScope _lifetimeScope;

        public QueryInvoker(ILifetimeScope lifetimeScope)
        {
            this._lifetimeScope = lifetimeScope;
        }

        public TQueryResult Query<TQueryResult>()
        {
            var handler = _lifetimeScope.Resolve<IHandleQuery<TQueryResult>>();
            return handler.Query();
        }

        public TQueryResult Query<TQuery, TQueryResult>(TQuery query)
        {
            var handler = _lifetimeScope.Resolve<IHandleQuery<TQuery, TQueryResult>>();
            return handler.Query(query);
        }
    }
}