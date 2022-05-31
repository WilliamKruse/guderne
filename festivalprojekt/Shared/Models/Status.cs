using System;

namespace festivalprojekt.Shared.Models
{
    public class Status
    {
        public int StatusId { get; set; }
        public string StatusNavn { get; set; }

        public Status()
        {
        }
        public override string ToString()
        {
            return $"{StatusNavn}";
        }
    }
}

