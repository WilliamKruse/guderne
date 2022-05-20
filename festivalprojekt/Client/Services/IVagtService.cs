using System;
using festivalprojekt.Shared.Models;


namespace festivalprojekt.Client.Services
{
	public interface IVagtService
	{
		public Task<VagtDTO> HentAlleVagter();

		public async Task<int> SletVagt(int Vagtid)

			public async Task<int> OpretVagt(VagtDTO NyVagt)

	}
}

