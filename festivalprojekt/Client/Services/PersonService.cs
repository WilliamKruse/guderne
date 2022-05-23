using System;
using System.Net.Http.Json;
using festivalprojekt.Shared.Models;

namespace festivalprojekt.Client.Services

{
	public class PersonService : IPersonService
	{
		private readonly HttpClient httpClient;

		public PersonService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public Task<PersonDTO[]?> HentAllePersoner()
        {
			var result = httpClient.GetFromJsonAsync<PersonDTO[]>("api/festivalapi/personer/hentallepersoner");
			return result;
        }

		public Task<PersonDTO[]?> HentPerson(int personid)
		{
			var result = httpClient.GetFromJsonAsync<PersonDTO[]>($"api/festivalapi/personer/hentperson?personid={personid}");
			return result;
		}

		public async Task<int> OpretPerson(PersonDTO NyPerson)
        {

			var response = await httpClient.PostAsJsonAsync("api/festivalapi/personer/opretperson", NyPerson);
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;

		}

		public async Task<int> OpdaterPerson(PersonDTO NyPerson)
        {

			var response = await httpClient.PutAsJsonAsync("api/festivalapi/personer/opdaterperson", NyPerson);
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;

		}
	}
}