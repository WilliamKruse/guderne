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

        [MinLength(1, ErrorMessage = "Felt skal udfyldes")]
        public string VagtTypeNavn { get; set; }

        [MinLength(1, ErrorMessage = "Skal have en beskrivelse")]
        public string? VagtTypeBeskrivelse { get; set; }

        [MinLength(1, ErrorMessage = "Skriv et område")]
        public string VagtTypeOmråde { get; set; }

        public int StatusId { get; set; }
    }
}