using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaIngresoBibliotecario.ApplicationCore.Entities
{
    [Table("Prestamo")]
    public class Prestamo
    {
        [Key]
        public Guid Id { get; set; }

        public Guid Isbn { get; set; }

        [Required]
        [StringLength(10)]
        public string IdentificacionUsuario { get; set; }

        public Usuario.Tipo TipoUsuario { get; set; }

        public DateTime FechaMaximaDevolucion { get; set; }
    }
}
