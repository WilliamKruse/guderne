using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace festivalprojekt.Shared.Models
{
    public class PersonDTO
    {
        public PersonDTO()
        {
        }

        public int[]? KompetenceId { get; set; }
        public string[]? KompetenceNavn { get; set; }
        public int? PersonId { get; set; }
        public int RolleId { get; set; }

        //Validering af fornavn
        [StringLength(40, ErrorMessage = "Navnet er for langt")]
        [MinLength(1, ErrorMessage = "Udfyld fornavn")]
        public string Fornavn { get; set; }


        //Validering af efternavn
        [StringLength(40, ErrorMessage = "Navnet er for langt")]
        [MinLength(1, ErrorMessage = "Udfyld efternavn")]
        public string Efternavn { get; set; }

        //Validering af fødselsdag
        public DateTime? RealF { get; set; }
        
        
        public string Fødselsdag { get; set; }


        //Valedering af email 
        [EmailAddress(ErrorMessage = "Indtast en valid email")]
        public string Email { get; set; }

        //Valerdering af telefonnummer
        [StringLength(8, MinimumLength = 8, ErrorMessage = "skriv dit 8-cifrede telefonnummer tak")]
        public string Telefon { get; set; }

        //Valerdering af kodeord
        [MinLength(8, ErrorMessage = "Dit kodeord skal mindst være på 8-cifre")]
        public string Kodeord { get; set; }

        public override string ToString()
        {
            return $"{KompetenceId}, {KompetenceNavn}, {PersonId}, {RolleId}";
        }


    }
}
