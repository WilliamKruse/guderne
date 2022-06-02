using System;
using System.Net.Http.Json;
using festivalprojekt.Shared.Models;


namespace festivalprojekt.Client.Services
{
	//definere klasse som implementer interfacet
	public class VagtService : IVagtService

	{
		//Variable
		private readonly HttpClient httpClient;

		//Constructor
		public VagtService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		//Metode som henter alle vagter. Her får man data fra api adressen som defineret i controlleren.
		public Task<VagtView[]?> HentAlleVagter(string streng, int id)
        {
			var result = httpClient.GetFromJsonAsync<VagtView[]>($"api/festivalapi/vagter/hentallevagter?streng={streng}&id={id}");
			return result;
		}

		//Metode som booker en vagt.Api adressen er defineret i controlleren.
		public async Task<int> BookVagt(BookVagtDTO Wrapper)
		{
			var response = await httpClient.PutAsJsonAsync($"api/festivalapi/vagter/bookvagt", Wrapper );
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;
		}

		//Async metode som sletter en vagt. Api adressen er defineret i controlleren.
		public async Task<int> SletVagt(int Vagtid)
		{
			var response = await httpClient.DeleteAsync($"api/festivalapi/vagter/{Vagtid}");
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;;
		}

		//Async metode der opretter en vagt. Api adressen er defineret i controlleren.
		public async Task<int> OpretVagt(VagtDTO NyVagt)
		{
			var response = await httpClient.PostAsJsonAsync("api/festivalapi/vagter/opretvagt", NyVagt);
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;
		}
	}
}
