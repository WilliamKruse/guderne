using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace festivalprojekt.Shared.Models
{
    public class PersonDTO
    {
        public int[]? KompetenceId { get; set; }
        public string[]? KompetenceNavn { get; set; }
        public int? PersonId { get; set; }
        public int RolleId { get; set; }

        //validering af fornavn
        //[Required(ErrorMessage = "Du skal udfylde dette felt")]
        //[StringLength(40, ErrorMessage = "Navnet er for langt")]
        public string Fornavn { get; set; }


        //validering af efternavn
        //[Required(ErrorMessage = "Du skal udfylde dette felt")]
        //[StringLength(40, ErrorMessage = "Navnet er for langt")]
        public string Efternavn { get; set; }

        ////validering af fødselsdag
        //[Required(ErrorMessage = "Du skal udfylde dette felt")]
        public DateTime Fødselsdag { get; set; }


        //Valedering af email 
        //[Required(ErrorMessage = "Du skal udfylde dette felt")]
        //[DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        //Valerdering af telefoner 
        //[Required(ErrorMessage = "Du skal udfylde dette felt")]
        //[StringLength(8, MinimumLength = 8, ErrorMessage = "skriv dit 8-cifrede telefonnummer tak")]
        public string Telefon { get; set; }


        public string Kodeord { get; set; }
         
    }
}