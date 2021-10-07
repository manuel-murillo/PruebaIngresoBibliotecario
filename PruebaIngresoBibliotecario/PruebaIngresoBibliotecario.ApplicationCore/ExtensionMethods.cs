using System;

namespace PruebaIngresoBibliotecario.ApplicationCore
{
    public static class ExtensionMethods
    {
        public static DateTime AgregarDiasLaborables(this DateTime fechaHora, ulong valor)
        {
            for (uint i = 0; i < valor; i++)
            {
                double diasAgregar = fechaHora.DayOfWeek == DayOfWeek.Friday
                    ? 3
                    : fechaHora.DayOfWeek == DayOfWeek.Saturday
                        ? 2
                        : 1;

                fechaHora = fechaHora.AddDays(diasAgregar);
            }

            return fechaHora;
        }
    }
}
