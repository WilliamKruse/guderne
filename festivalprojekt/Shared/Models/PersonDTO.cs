using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalprojekt.Shared.Models
{
    public class PersonDTO
    {
        public int[]? KompetenceId { get; set; }
        public string[]? KompetenceNavn { get; set; }
        public int? PersonId { get; set; }
        public int RolleId { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Kodeord { get; set; }
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public DateTime Fødselsdag { get; set; }
        
       
    }
}