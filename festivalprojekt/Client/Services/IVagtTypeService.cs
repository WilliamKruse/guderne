using System;
using festivalprojekt.Shared.Models;

namespace festivalprojekt.Client.Services
{
	public interface IVagtTypeService
	{
		Task<VagtTypeDTO[]?> HentAlleVagtTyper();

		Task<int> SletVagtType(int VagtTypeId);

		Task<int> OpretVagtType(VagtTypeDTO NyVagtType);
	}
}