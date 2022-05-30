using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using festivalprojekt.Shared.Models;


namespace festivalprojekt.Server.Models
{
	public interface IVagtTypeReposityDapper
	{
		Task<IEnumerable<VagtTypeDTO>> HentAlleVagtTyper();

		void OpretVagtType(VagtTypeDTO NyVagtType);

		void SletVagtType(int VagtTypeId);

		void OpdaterVagtType(VagtTypeDTO NyVagtType);
	}
}

