namespace LFM.ApplicationServices
{
    public interface IHandleCommand<in TCommand, out TResponse>
    {
        TResponse Execute(TCommand command);
    }

    public interface IHandleCommand<in T>
    {
        void Execute(T command);
    }
}