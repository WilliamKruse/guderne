using System;
using System.Net.Http.Json;
using festivalprojekt.Shared.Models;


namespace festivalprojekt.Client.Services
{
	public class VagtTypeService : IVagtTypeService
	{
		private readonly HttpClient httpClient;

		public VagtTypeService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public Task<VagtTypeDTO[]?> HentAlleVagtTyper()
		{
			var result = httpClient.GetFromJsonAsync<VagtTypeDTO[]>("api/festivalapi/vagttyper/hentallevagttyper");
			return result;
		}

		public async Task<int> OpretVagtType(VagtTypeDTO NyVagtType)
		{

			var response = await httpClient.PostAsJsonAsync("api/festivalapi/vagttyper/opretvagttype", NyVagtType);
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;

		}
		public async Task<int> SletVagtType(int VagtTypeId)
		{
			var response = await httpClient.DeleteAsync($"api/festivalapi/vagttyper/{VagtTypeId}");
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;
		}

	}
}

