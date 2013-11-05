using Autofac;

namespace LFM.ApplicationServices
{
    /// <summary>
    /// The command invoker is responsible for write operation, each of these 
    /// commands should be an atomic unit, so it hanldes the unit of work.
    /// </summary>
    public class CommandInvoker : ICommandInvoker
    {
        private readonly ILifetimeScope _lifetimeScope;

        public CommandInvoker(ILifetimeScope lifetimeScope) 
        {
            _lifetimeScope = lifetimeScope;
        }

        public void Execute<TCommand>(TCommand command)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var handler = _lifetimeScope.Resolve<IHandleCommand<TCommand>>();
                handler.Execute(command);
                unitOfWork.Complete();
            }
        }

        public TResponse Execute<TCommand, TResponse>(TCommand command)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var handler = _lifetimeScope.Resolve<IHandleCommand<TCommand, TResponse>>();
                var result = handler.Execute(command);
                unitOfWork.Complete();
                return result;
            }
        }
    }
}