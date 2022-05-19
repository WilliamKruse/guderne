using System;
using festivalprojekt.Shared.Models;

namespace festivalprojekt.Client.Services

{
	public interface IPersonService
	{
		Task<PersonDTO[]?> HentAllePersoner();
	}
}

