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
	}
}

