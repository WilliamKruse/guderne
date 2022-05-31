using System;
using festivalprojekt.Shared.Models;

namespace festivalprojekt.Client.Services

{
	public interface IPersonService
	{
		Task<Roller[]?> HentAlleRoller();

		Task<Kompetencer[]?> HentAlleKompetencer();

		Task<PersonDTO[]?> HentAllePersoner();

		Task<PersonDTO[]?> HentPerson(int personid);

		Task<int> OpretPerson(PersonDTO NyPerson);

		Task<int> OpdaterPerson(PersonDTO NyPerson);
	}
}


