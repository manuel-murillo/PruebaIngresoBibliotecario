using Microsoft.EntityFrameworkCore;
using PruebaIngresoBibliotecario.SharedKernel.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Infrastructure.Repositories
{
    public class DataRepository : IRepository
    {
        private readonly PersistenceContext _context;

        public DataRepository(PersistenceContext context)
        {
            _context = context;
        }

        public bool Existe<T>(Expression<Func<T, bool>> filtro) where T : class
        {
            return _context.Set<T>().Any(filtro);
        }

        public Task<bool> ExisteAsync<T>(Expression<Func<T, bool>> filtro) where T : class
        {
            return _context.Set<T>().AnyAsync(filtro);
        }

        public T Adicionar<T>(T entidad) where T : class
        {
            _context.Set<T>().Add(entidad);

            _context.SaveChanges();

            return entidad;
        }

        public async Task<T> AdicionarAsync<T>(T entidad) where T : class
        {
            await _context.Set<T>().AddAsync(entidad);

            await _context.SaveChangesAsync();

            return entidad;
        }

        public T Encontrar<T>(Expression<Func<T, bool>> filtro) where T : class
        {
            return _context.Set<T>().FirstOrDefault(filtro);
        }

        public Task<T> EncontrarAsync<T>(Expression<Func<T, bool>> filtro) where T : class
        {
            return _context.Set<T>().FirstOrDefaultAsync(filtro);
        }
    }
}
