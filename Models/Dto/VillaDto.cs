using System.ComponentModel.DataAnnotations;

namespace Magic_API.Models.Dto
{
    public class VillaDto
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string ?Nombre { get; set; }
        public int Ocupantes { get; set; }
        public int MetrosCuadrados { get; set; }

    }
}
