using System;
using festivalprojekt.Shared.Models;


namespace festivalprojekt.Client.Services
{
	//laver interfacet med metoder til klasserne
	public interface IVagtService
	{
		Task<VagtView[]?> HentAlleVagter(string a, int b);
		Task<int> SletVagt(int Vagtid);
		Task<int> OpretVagt(VagtDTO NyVagt);
		Task<int> BookVagt(BookVagtDTO Wrapper);
	}
}

