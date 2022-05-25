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

		public Task<Roller[]?> HentAlleRoller()
		{
			var result = httpClient.GetFromJsonAsync<Roller[]>("api/festivalapi/personer/hentalleroller");
			return result;
		}


		public Task<Kompetencer[]?> HentAlleKompetencer()
		{
			var result = httpClient.GetFromJsonAsync<Kompetencer[]>("api/festivalapi/personer/hentallekompetencer");
			return result;
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
			Console.WriteLine("service siger hej - opret person");
			var response = await httpClient.PostAsJsonAsync("api/festivalapi/personer/opret", NyPerson);
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