using System;
using System.ComponentModel.DataAnnotations;

namespace PruebaIngresoBibliotecario.ApplicationCore.Entities
{
    public class PrestamoSolicitud
    {
        public string Isbn { get; set; }

        [StringLength(10)]
        public string IdentificacionUsuario { get; set; }

        public Usuario.Tipo TipoUsuario { get; set; }
    }
}
