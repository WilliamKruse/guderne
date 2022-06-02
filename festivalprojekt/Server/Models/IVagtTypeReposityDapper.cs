using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using festivalprojekt.Shared.Models;


namespace festivalprojekt.Server.Models
{
	//laver interfacet med metoder til klasserne
	public interface IVagtTypeReposityDapper
	{
		Task<IEnumerable<Status>> HentAlleStatus();
		Task<IEnumerable<VagtTypeDTO>> HentAlleVagtTyper();
		void OpretVagtType(VagtTypeDTO NyVagtType);
		void SletVagtType(int VagtTypeId);
		void OpdaterVagtType(VagtTypeDTO NyVagtType);
	}
}

