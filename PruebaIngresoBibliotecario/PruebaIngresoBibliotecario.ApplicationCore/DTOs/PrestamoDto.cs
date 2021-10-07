using PruebaIngresoBibliotecario.ApplicationCore.Entities;
using System;

namespace PruebaIngresoBibliotecario.ApplicationCore.DTOs
{
    public class PrestamoDto
    {
        public Guid Id { get; set; }

        public Guid Isbn { get; set; }

        public string IdentificacionUsuario { get; set; }

        public Usuario.Tipo TipoUsuario { get; set; }

        public string FechaMaximaDevolucion { get; set; }
    }
}
