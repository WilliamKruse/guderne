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

		public Task<VagtView[]?> HentAlleVagter(string streng, int id)
        {
			var result = httpClient.GetFromJsonAsync<VagtView[]>($"api/festivalapi/vagter/hentallevagter?streng={streng}&id={id}");
			return result;
		}

		
		public async Task<int> BookVagt(int VagtId, int PersonId)
		{

			var response = await httpClient.PutAsJsonAsync("api/festivalapi/vagter/bookvagt", (VagtId, PersonId));
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;

		}

		public async Task<int> SletVagt(int Vagtid)
		{
			var response = await httpClient.DeleteAsync($"api/festivalapi/vagter/{Vagtid}");
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;;
		}

		public async Task<int> OpretVagt(VagtDTO NyVagt)
		{

			var response = await httpClient.PostAsJsonAsync("api/festivalapi/vagter/opretvagt", NyVagt);
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;

		}

	}
}
