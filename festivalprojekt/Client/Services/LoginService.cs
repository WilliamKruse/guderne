﻿using System;
using System.Net.Http.Json;
using festivalprojekt.Shared.Models;
using festivalprojekt.Client.Services;


namespace festivalprojekt.Client.Services
{


    public class LoginService :ILoginService
    {
        private readonly HttpClient httpClient;

        public LoginService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<PersonDTO>> HentLoginPerson(LoginDTO login)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<PersonDTO>>($"api/festivalapi/personer/login?email={login.Email}&kode={login.Kode}");
            
        }
    }
   
   
}
