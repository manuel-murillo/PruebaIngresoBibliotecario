using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.SharedKernel.Interfaces
{
    public interface IRepository
    {
        bool Existe<T>(Expression<Func<T, bool>> filtro) where T : class;

        Task<bool> ExisteAsync<T>(Expression<Func<T, bool>> filtro) where T : class;

        T Adicionar<T>(T entidad) where T : class;

        Task<T> AdicionarAsync<T>(T entidad) where T : class;

        T Encontrar<T>(Expression<Func<T, bool>> filtro = null) where T : class;

        Task<T> EncontrarAsync<T>(Expression<Func<T, bool>> filtro = null) where T : class;
    }
}
