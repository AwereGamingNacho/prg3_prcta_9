using prg3_prcta_9.Controllers;
using prg3_prcta_9.Models.Dto;

namespace prg3_prcta_9.Data
{
    public class EjercicioStore
    {
        public static List<EjercicioDto> EjercicioLista = new List<EjercicioDto>
        {
            new EjercicioDto {Id = 1, Nombre = "Vista a la Pisina", Ocupantes = 3, MetrosCuadrados = 50},
            new EjercicioDto {Id = 2, Nombre = "Vista a la Playa", Ocupantes = 2, MetrosCuadrados = 100}
        };
    }
}
