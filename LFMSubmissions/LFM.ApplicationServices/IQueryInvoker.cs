namespace LFM.ApplicationServices
{
    public interface IQueryInvoker
    {
        TQueryResult Query<TQueryResult>();
        TQueryResult Query<TQuery, TQueryResult>(TQuery query);
    }
}