using System;
using System.Net.Http.Json;
using festivalprojekt.Shared.Models;


namespace festivalprojekt.Client.Services
{
	//definere klasse som implementer interfacet
	public class VagtTypeService : IVagtTypeService
	{
		//Variable
		private readonly HttpClient httpClient;

		//Constructor
		public VagtTypeService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		//Metode som henter alle status. Her får man data fra api adressen som defineret i controlleren.
		public Task<Status[]?> HentAlleStatus()
		{
			var result = httpClient.GetFromJsonAsync<Status[]>("api/festivalapi/vagttyper/hentallestatus");
			return result;
		}

		//Metode som henter alle vagt typer. Her får man data fra api adressen som defineret i controlleren.
		public Task<VagtTypeDTO[]?> HentAlleVagtTyper()
		{
			var result = httpClient.GetFromJsonAsync<VagtTypeDTO[]>("api/festivalapi/vagttyper/hentallevagttyper");
			return result;
		}

		//Async metode der opretter en vagt type. Api adressen er defineret i controlleren.

		public async Task<int> OpretVagtType(VagtTypeDTO NyVagtType)
		{
			var response = await httpClient.PostAsJsonAsync("api/festivalapi/vagttyper/opretvagttype", NyVagtType);
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;
		}

		//Async metode der sletter en vagt type. Api adressen er defineret i controlleren.
		public async Task<int> SletVagtType(int VagtTypeId)
		{
			var response = await httpClient.DeleteAsync($"api/festivalapi/vagttyper/{VagtTypeId}");
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;
		}

		//Async metode der opdater en vagt type. Api adressen er defineret i controlleren.
		public async Task<int> OpdaterVagtType(VagtTypeDTO NyVagtType)
		{
			var response = await httpClient.PutAsJsonAsync("api/festivalapi/vagttyper/opdatervagttype", NyVagtType);
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;
		}
	}
}

