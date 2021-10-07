using System;

namespace PruebaIngresoBibliotecario.ApplicationCore.DTOs
{
    public class PrestamoSolicitudDto
    {
        public Guid Id { get; set; }

        public string FechaMaximaDevolucion { get; set; }
    }
}
