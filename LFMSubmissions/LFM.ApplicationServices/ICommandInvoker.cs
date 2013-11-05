namespace LFM.ApplicationServices
{
    public interface ICommandInvoker
    {
        void Execute<TCommand>(TCommand command);
        TResponse Execute<TCommand, TResponse>(TCommand command);
    }
}