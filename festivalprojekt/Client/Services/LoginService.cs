using System;
using System.Net.Http.Json;
using festivalprojekt.Shared.Models;
using festivalprojekt.Client.Services;


namespace festivalprojekt.Client.Services
{
    //definere klasse som implementer interfacet
    public class LoginService :ILoginService
    {
        //Variable 
        private readonly HttpClient httpClient;

        //Constructor
        public LoginService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        //Async metode der henter login. Her får man data fra api adressen som defineret i controlleren.
        public async Task<IEnumerable<PersonDTO>> HentLoginPerson(LoginDTO login)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<PersonDTO>>($"api/festivalapi/personer/login?email={login.Email}&kode={login.Kode}");          
        }
    }  
}
