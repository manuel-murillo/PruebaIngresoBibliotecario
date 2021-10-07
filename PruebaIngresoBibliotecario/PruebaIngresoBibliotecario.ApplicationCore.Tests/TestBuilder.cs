using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PruebaIngresoBibliotecario.ApplicationCore.Services;
using PruebaIngresoBibliotecario.Infrastructure;
using PruebaIngresoBibliotecario.Infrastructure.Repositories;
using System.Collections.Generic;

namespace ApplicationCore.Test
{
    public abstract class IntegrationTestBuilder
    {
        protected PrestamoService TestService;

        protected IntegrationTestBuilder()
        {
            BootstrapTestingSuite();
        }

        protected void BootstrapTestingSuite()
        {
            PersistenceContext context = new TestDbContextFactory()
                .CreateDbContext("UseInMemoryDatabase");

            TestService = new PrestamoService(new DataRepository(context));
        }

        private class TestDbContextFactory : IDesignTimeDbContextFactory<PersistenceContext>
        {
            public PersistenceContext CreateDbContext(params string[] args)
            {
                DbContextOptions<PersistenceContext> options = new DbContextOptionsBuilder<PersistenceContext>()
                    .UseInMemoryDatabase("PruebaIngreso").Options;

                IConfiguration config = new ConfigurationBuilder()
                    .AddInMemoryCollection(new Dictionary<string, string>())
                    .Build();

                return new PersistenceContext(options, config);
            }
        }
    }
}
