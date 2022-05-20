using System;
using System.Net.Http.Json;
using festivalprojekt.Shared.Models;


namespace festivalprojekt.Client.Services
{

	public class VagtService : IVagtService

	{
		private readonly HttpClient httpClient;

		public VagtService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public Task<VagtDTO> HentAlleVagter()
        {
			var result = httpClient.GetFromJsonAsync<VagtDTO>("api/festivalapi/personer/getvagter");
			return result;
		}

		
		public async Task<int> BookVagt(int VagtId, int PersonId)
		{

			var response = await httpClient.PutAsJsonAsync("api/festivalapi/bookvagt", (VagtId, PersonId));
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;

		}

		public async Task<int> SletVagt(int Vagtid)
		{
			var response = await httpClient.DeleteAsync("api/festivalapi/sletvagt" + Vagtid);
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;
		}

		public async Task<int> OpretVagt(VagtDTO NyVagt)
		{

			var response = await httpClient.PostAsJsonAsync("api/festivalapi/opretvagt", NyVagt);
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;

		}

	}
}
