using System;
using festivalprojekt.Shared.Models;

namespace festivalprojekt.Client.Services
{
	public interface IVagtTypeService
	{
		Task<Status[]?> HentAlleStatus();

		Task<VagtTypeDTO[]?> HentAlleVagtTyper();

		Task<int> SletVagtType(int VagtTypeId);

		Task<int> OpretVagtType(VagtTypeDTO NyVagtType);

		Task<int> OpdaterVagtType(VagtTypeDTO NyVagtType);
	}
}