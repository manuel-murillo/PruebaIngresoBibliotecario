using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PruebaIngresoBibliotecario.ApplicationCore.DTOs;
using PruebaIngresoBibliotecario.ApplicationCore.Entities;
using PruebaIngresoBibliotecario.ApplicationCore.Interfaces;
using System;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace PruebaIngresoBibliotecario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamoController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PrestamoSolicitudDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostPrestamoAsync(PrestamoSolicitud prestamoSolicitud)
        {
            string usuarioId = prestamoSolicitud.IdentificacionUsuario;

            if (string.IsNullOrEmpty(usuarioId) || !Guid.TryParse(prestamoSolicitud.Isbn, out _))
            {
                return ValidationProblem();
            }

            if (prestamoSolicitud.TipoUsuario == Usuario.Tipo.UsuarioInvitado && await _prestamoService.TienePrestamosAsync(prestamoSolicitud.IdentificacionUsuario))
            {
                return new ContentResult
                {
                    Content = JsonSerializer.Serialize(new
                    {
                        mensaje = $"El usuario con identificacion {usuarioId} ya tiene un libro prestado por lo cual no se le puede realizar otro prestamo"
                    }),
                    ContentType = MediaTypeNames.Application.Json,
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            Prestamo prestamo = await _prestamoService.AdicionarPrestamoAsync(prestamoSolicitud);

            PrestamoSolicitudDto prestamoSolicitudDto = new PrestamoSolicitudDto
            {
                Id = prestamo.Id,
                FechaMaximaDevolucion = prestamo.FechaMaximaDevolucion.ToShortDateString()
            };

            return Ok(prestamoSolicitudDto);
        }

        [HttpGet("{idPrestamo}")]
        [ProducesResponseType(typeof(PrestamoDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IStatusCodeActionResult> GetPrestamoAsync(Guid idPrestamo)
        {
            PrestamoDto prestamo = await _prestamoService.EncontrarPrestamoAsync(idPrestamo);

            if (prestamo == null)
            {
                return new ContentResult
                {
                    Content = JsonSerializer.Serialize(new
                    {
                        mensaje = $"El prestamo con id {idPrestamo} no existe"
                    }),
                    ContentType = MediaTypeNames.Application.Json,
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }

            return Ok(prestamo);
        }
    }
}
