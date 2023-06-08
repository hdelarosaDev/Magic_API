using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magic_API.Models
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(80)]
        public string ?Nombre { get; set; }
        [MaxLength(1000)]
        public string ?Detalle { get; set; }

        [Required]
        public double Tarifa { get; set; }
        public int Ocupantes { get; set; }

        public double MetrosCuadrados { get; set; }
        public string ?ImagenUrl { get; set; }
        public string ?Amenidad { get; set; }

        public int Niveles { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}
