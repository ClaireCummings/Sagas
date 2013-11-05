namespace LFM.ApplicationServices
{
    public interface IHandleQuery<out TQueryResult>
    {
        TQueryResult Query();
    }

    public interface IHandleQuery<in TQuery, out TQueryResult>
    {
        TQueryResult Query(TQuery query);
    }
}