using System.ComponentModel.DataAnnotations;

namespace festivalprojekt.Shared.Models
{
    public class LoginDTO
    {
        [EmailAddress(ErrorMessage ="Indtast en valid email")]
        public string Email { get; set; }
        public string Kode { get; set; }
    }
}
