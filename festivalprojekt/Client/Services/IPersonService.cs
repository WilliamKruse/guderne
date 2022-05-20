using System;
using festivalprojekt.Shared.Models;

namespace festivalprojekt.Client.Services

{
	public interface IPersonService
	{
		Task<PersonDTO[]?> HentAllePersoner();

		Task<int> OpretPerson(PersonDTO NyPerson);

		Task<int> OpdaterPerson(PersonDTO NyPerson);
	}
}


