using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace festivalprojekt.Shared.Models
{
    public class VagtTypeDTO
    {
        public int? VagtTypeID { get; set; }

        [Required(ErrorMessage = "Tilføj vagttype navn")]
        public string VagtTypeNavn { get; set; }

        [Required(ErrorMessage = "Tilføj vagttype beskrivelse")]
        public string? VagtTypeBeskrivelse { get; set; }

        [Required(ErrorMessage = "Tilføj vagt type område")]
        public string VagtTypeOmråde { get; set; }

        public int StatusId { get; set; }
    }
}