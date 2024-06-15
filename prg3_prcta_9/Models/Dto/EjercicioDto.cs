using System.ComponentModel.DataAnnotations;

namespace prg3_prcta_9.Models.Dto
{
    public class EjercicioDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nombre { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }
    }
}
