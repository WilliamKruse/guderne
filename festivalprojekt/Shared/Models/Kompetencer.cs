using System;
namespace festivalprojekt.Shared.Models
{
	public class Kompetencer
	{
		public int KompetenceId { get; set; }
		public string KompetenceNavn { get; set; }
		public bool Checker { get; set; } = false;

		public Kompetencer()
		{
		}
	}
}

