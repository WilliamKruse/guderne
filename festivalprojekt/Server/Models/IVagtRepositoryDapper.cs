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
	public interface IVagtRepositoryDapper
	{
		Task<IEnumerable<VagtView>> HentAlleVagter(string a, int b);
		void BookVagt(int VagtId, int PersonId);
		void SletVagt(int VagtId);
		void OpretVagt(VagtDTO NyVagt);
	}
}

