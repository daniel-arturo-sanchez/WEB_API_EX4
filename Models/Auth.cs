using System.ComponentModel.DataAnnotations;

namespace API_WEB_Ejercicio3.Models
{
    public class Auth
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
