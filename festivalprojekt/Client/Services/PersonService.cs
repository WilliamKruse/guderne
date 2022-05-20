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
			var result = httpClient.GetFromJsonAsync<PersonDTO[]>("api/festivalapi/personer/getthat");
			return result;
        }

		public async Task<int> OpretPerson(PersonDTO NyPerson)
        {

			var response = await httpClient.PostAsJsonAsync("api/festivalapi/personer", NyPerson);
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;

		}

		public async Task<int> OpdaterPerson(PersonDTO NyPerson)
        {

			var response = await httpClient.PutAsJsonAsync("api/festivalapi/personer", NyPerson);
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;

		}
	}
}