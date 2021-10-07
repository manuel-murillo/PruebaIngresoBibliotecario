using PruebaIngresoBibliotecario.ApplicationCore.DTOs;
using PruebaIngresoBibliotecario.ApplicationCore.Entities;
using System;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.ApplicationCore.Interfaces
{
    public interface IPrestamoService
    {
        bool TienePrestamos(string id);

        Task<bool> TienePrestamosAsync(string id);

        Prestamo AdicionarPrestamo(PrestamoSolicitud prestamoSolicitud);

        Task<Prestamo> AdicionarPrestamoAsync(PrestamoSolicitud prestamoSolicitud);

        PrestamoDto EncontrarPrestamo(Guid idPrestamo);

        Task<PrestamoDto> EncontrarPrestamoAsync(Guid idPrestamo);
    }
}
