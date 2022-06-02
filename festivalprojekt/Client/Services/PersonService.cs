using System;
using System.Net.Http.Json;
using festivalprojekt.Shared.Models;

namespace festivalprojekt.Client.Services

{
	//definere klasse som implementer interfacet
	public class PersonService : IPersonService
	{
		//Varible 
		private readonly HttpClient httpClient;

		//Constructor 
		public PersonService(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		//Metode der henter alle roller. Her får man data fra api adressen som defineret i controlleren.
		public Task<Roller[]?> HentAlleRoller()
		{
			var result = httpClient.GetFromJsonAsync<Roller[]>("api/festivalapi/personer/hentalleroller");
			return result;
		}

		//Metode der henter alle kompetencer. Her får man data fra api adressen som defineret i controlleren.
		public Task<Kompetencer[]?> HentAlleKompetencer()
		{
			var result = httpClient.GetFromJsonAsync<Kompetencer[]>("api/festivalapi/personer/hentallekompetencer");
			return result;
		}

		//Metode der henter alle personer. Her får man data fra api adressen som defineret i controlleren.
		public Task<PersonDTO[]?> HentAllePersoner()
        {
			var result = httpClient.GetFromJsonAsync<PersonDTO[]>("api/festivalapi/personer/hentallepersoner");
			return result;
        }

		//Metode der henter en person. Her får man data fra api adressen som defineret i controlleren.
		public Task<PersonDTO[]?> HentPerson(int personid)
		{
			var result = httpClient.GetFromJsonAsync<PersonDTO[]>($"api/festivalapi/personer/hentperson?personid={personid}");
			return result;
		}

		//Async Metode der opretter en personer. Api adressen er defineret i controlleren.
		public async Task<int> OpretPerson(PersonDTO NyPerson)
        {
			var response = await httpClient.PostAsJsonAsync("api/festivalapi/personer/opret", NyPerson);
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;
		}

		//Async metode der opdater en person. Api adressen er defineret i controlleren.
		public async Task<int> OpdaterPerson(PersonDTO NyPerson)
        {
			var response = await httpClient.PutAsJsonAsync("api/festivalapi/personer/opdaterperson", NyPerson);
			var responseStatusCode = response.StatusCode;
			return (int)responseStatusCode;
		}
	}
}