using PruebaIngresoBibliotecario.ApplicationCore.DTOs;
using PruebaIngresoBibliotecario.ApplicationCore.Entities;
using PruebaIngresoBibliotecario.ApplicationCore.Interfaces;
using PruebaIngresoBibliotecario.SharedKernel;
using PruebaIngresoBibliotecario.SharedKernel.Interfaces;
using System;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.ApplicationCore.Services
{
    public class PrestamoService : Service, IPrestamoService
    {
        public PrestamoService(IRepository repository) : base(repository) { }

        public bool TienePrestamos(string identificacionUsuario)
        {
            return Repository.Existe<Prestamo>(e => e.IdentificacionUsuario == identificacionUsuario);
        }

        public Task<bool> TienePrestamosAsync(string identificacionUsuario)
        {
            return Repository.ExisteAsync<Prestamo>(e => e.IdentificacionUsuario == identificacionUsuario);
        }

        private Prestamo ObtenerPrestamo(PrestamoSolicitud prestamoSolicitud)
        {
            ulong diasMaximosDevolucion = prestamoSolicitud.TipoUsuario switch
            {
                Usuario.Tipo.UsuarioAfilado => 10,
                Usuario.Tipo.UsuarioEmpleadoBiblioteca => 8,
                _ => 7,
            };

            Prestamo prestamo = new Prestamo
            {
                Isbn = Guid.Parse(prestamoSolicitud.Isbn),
                IdentificacionUsuario = prestamoSolicitud.IdentificacionUsuario,
                TipoUsuario = prestamoSolicitud.TipoUsuario,
                FechaMaximaDevolucion = DateTime.Today.AgregarDiasLaborables(diasMaximosDevolucion)
            };

            return prestamo;
        }

        public Prestamo AdicionarPrestamo(PrestamoSolicitud prestamoSolicitud)
        {
            return Repository.Adicionar(ObtenerPrestamo(prestamoSolicitud));
        }

        public Task<Prestamo> AdicionarPrestamoAsync(PrestamoSolicitud prestamoSolicitud)
        {
            return Repository.AdicionarAsync(ObtenerPrestamo(prestamoSolicitud));
        }

        private PrestamoDto ConvertirPrestamoAPrestamoDto(Prestamo prestamo)
        {
            return prestamo == null
                ? null
                : new PrestamoDto
                {
                    Id = prestamo.Id,
                    Isbn = prestamo.Isbn,
                    IdentificacionUsuario = prestamo.IdentificacionUsuario,
                    TipoUsuario = prestamo.TipoUsuario,
                    FechaMaximaDevolucion = prestamo.FechaMaximaDevolucion.ToShortDateString()
                };
        }

        public PrestamoDto EncontrarPrestamo(Guid idPrestamo)
        {
            Prestamo prestamo = Repository.Encontrar<Prestamo>(p => p.Id == idPrestamo);

            return ConvertirPrestamoAPrestamoDto(prestamo);
        }

        public async Task<PrestamoDto> EncontrarPrestamoAsync(Guid idPrestamo)
        {
            Prestamo prestamo = await Repository.EncontrarAsync<Prestamo>(p => p.Id == idPrestamo);

            return ConvertirPrestamoAPrestamoDto(prestamo);
        }
    }
}
