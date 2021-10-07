using PruebaIngresoBibliotecario.SharedKernel.Interfaces;

namespace PruebaIngresoBibliotecario.SharedKernel
{
    public abstract class Service
    {
        protected IRepository Repository { get; }

        protected Service(IRepository repository)
        {
            Repository = repository;
        }
    }
}
