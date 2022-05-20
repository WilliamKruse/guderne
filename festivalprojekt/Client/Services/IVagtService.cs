using System;
using festivalprojekt.Shared.Models;


namespace festivalprojekt.Client.Services
{
	public interface IVagtService
	{
		Task<VagtView[]?> HentAlleVagter(string a, int b);

		Task<int> SletVagt(int Vagtid);

		Task<int> OpretVagt(VagtDTO NyVagt);

	}
}

