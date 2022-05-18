using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalprojekt.Shared.Models
{
    public class VagtDTO
    {
        public int? VagtId { get; set; }
        public int VagtTypeId { get; set; }
        public DateTime StartTid { get; set; }
        public DateTime SlutTid { get; set; }
        public int? PersonId { get; set; }
    }
}
