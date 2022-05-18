using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalprojekt.Shared.Models
{
    public class VagtTypeDTO
    {
        public int? VagtTypeID { get; set; }
        public string VagtTypeNavn { get; set; }

        public string? VagtTypeBeskrivelse { get; set; }
        public string VagtTypeOmråde { get; set; }
    }
}