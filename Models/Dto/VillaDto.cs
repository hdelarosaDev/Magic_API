using System.ComponentModel.DataAnnotations;

namespace Magic_API.Models.Dto
{
    public class VillaDto
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string ?Nombre { get; set; }
      
        public string? Detalle { get; set; }

        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }

        public double MetrosCuadrados { get; set; }
        public string? ImagenUrl { get; set; }
        public string? Amenidad { get; set; }


    }
}
