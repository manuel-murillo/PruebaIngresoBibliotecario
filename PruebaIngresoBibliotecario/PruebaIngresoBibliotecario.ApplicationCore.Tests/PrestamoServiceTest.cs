using FluentAssertions;
using PruebaIngresoBibliotecario.ApplicationCore.DTOs;
using PruebaIngresoBibliotecario.ApplicationCore.Entities;
using System;
using Xunit;

namespace ApplicationCore.Test
{
    public class PrestamoServiceTest : IntegrationTestBuilder
    {
        [Fact]
        public void AdicionarPrestamoPrueba()
        {
            Usuario.Tipo tipoUsuario = Usuario.Tipo.UsuarioAfilado;
            string identificacionUsuario = "123456789";
            string isbn = Guid.NewGuid().ToString();

            PrestamoSolicitud solicitudPrestamo = new PrestamoSolicitud
            {
                TipoUsuario = tipoUsuario,
                IdentificacionUsuario = identificacionUsuario,
                Isbn = isbn
            };

            Prestamo prestamo = TestService.AdicionarPrestamo(solicitudPrestamo);

            prestamo.Id.Should().NotBeEmpty();
            prestamo.Isbn.Should().Be(isbn);
            prestamo.IdentificacionUsuario.Should().Be(identificacionUsuario);
            prestamo.TipoUsuario.Should().Be(tipoUsuario);
            prestamo.FechaMaximaDevolucion.Should().BeAfter(DateTime.Now);
        }

        [Fact]
        public void EncontrarPrestamoPrueba()
        {
            Usuario.Tipo tipoUsuario = Usuario.Tipo.UsuarioEmpleadoBiblioteca;
            string identificacionUsuario = "0123456789";
            string isbn = Guid.NewGuid().ToString();

            PrestamoSolicitud solicitudPrestamo = new PrestamoSolicitud
            {
                TipoUsuario = tipoUsuario,
                IdentificacionUsuario = identificacionUsuario,
                Isbn = isbn
            };

            Prestamo prestamo = TestService.AdicionarPrestamo(solicitudPrestamo);

            Guid idPrestamo = prestamo.Id;

            PrestamoDto prestamoDto = TestService.EncontrarPrestamo(idPrestamo);

            prestamoDto.Id.Should().Be(idPrestamo);
            prestamoDto.Isbn.Should().Be(isbn);
            prestamoDto.IdentificacionUsuario.Should().Be(identificacionUsuario);
            prestamoDto.TipoUsuario.Should().Be(tipoUsuario);
            prestamoDto.FechaMaximaDevolucion.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void TienePrestamosPrueba()
        {
            string identificacionUsuario = "1234567890";

            PrestamoSolicitud solicitudPrestamo = new PrestamoSolicitud
            {
                TipoUsuario = Usuario.Tipo.UsuarioInvitado,
                IdentificacionUsuario = identificacionUsuario,
                Isbn = Guid.NewGuid().ToString()
            };

            TestService.AdicionarPrestamo(solicitudPrestamo);

            TestService.TienePrestamos(identificacionUsuario).Should().BeTrue();
        }
    }

}
