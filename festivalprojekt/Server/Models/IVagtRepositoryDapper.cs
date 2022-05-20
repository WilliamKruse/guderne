using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using festivalprojekt.Shared.Models;


namespace festivalprojekt.Server.Models
{
	public interface IVagtRepositoryDapper
	{

		Task<IEnumerable<VagtDTO>> HentAlleVagter();

		void BookVagt(int VagtId, int PersonId);

		void SletVagt(int VagtId);

		void OpretVagt(VagtDTO NyVagt);

	}
}

