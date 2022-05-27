using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace festivalprojekt.Shared.Models
{
    public class Roller
    {
        public int RolleId { get; set; }
        public string RolleNavn { get; set; }
        public bool Checker { get; set; }

        public Roller() 
        {
            if (this.RolleId == 1)
            {
                Checker = true;
            }
            else
            {
                Checker = false;
            }
        }

    }
}
