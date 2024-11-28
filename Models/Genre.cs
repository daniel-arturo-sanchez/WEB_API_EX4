using System.ComponentModel.DataAnnotations;

namespace API_WEB_Ejercicio3.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Título")]
        [Required(ErrorMessage = "Este campo debe ser rellenado")]
        [StringLength(50, ErrorMessage = "Este campo debe tener entre {0} y {2} caracteres", MinimumLength = 5)]
        public string Title { get; set; }
        public List<Game> Games { get; set; }
    }
}
