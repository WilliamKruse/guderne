using System;
using festivalprojekt.Shared.Models;

namespace festivalprojekt.Client.Services

{
	//laver interfacet med metoder til klasserne
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


