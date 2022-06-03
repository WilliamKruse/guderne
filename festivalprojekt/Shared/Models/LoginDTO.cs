using System.ComponentModel.DataAnnotations;

namespace festivalprojekt.Shared.Models
{
    public class LoginDTO
    {
        [Required(ErrorMessage ="Indtast email")]
        [EmailAddress(ErrorMessage ="Indtast en valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Indtast kode")]
        public string Kode { get; set; }
    }
}
