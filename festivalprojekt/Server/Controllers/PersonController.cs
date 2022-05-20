using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections;
using festivalprojekt.Server.Models;
using festivalprojekt.Shared.Models;



namespace festivalprojekt.Server.Controllers
{
    [ApiController]
    [Route("api/festivalapi/personer")]

    public class PersonController : ControllerBase
    {
        private readonly IPersonRepositoryDapper repo = new PersonRepositoryDapper();

        public PersonController(IPersonRepositoryDapper personRepositoryDapper)
        {
            if (repo == null && personRepositoryDapper != null)
            {
                repo = personRepositoryDapper;
                Console.WriteLine("Repository Initialized");
            }
        }

        [HttpGet("getthat")]
        public async Task<IEnumerable<PersonDTO>> HentAllePersoner()
        {
            return await repo.HentAllePersoner();
        }

        [HttpPost()]
        public async void OpretPerson(PersonDTO NyPerson)
        {
            repo.OpretPerson(NyPerson);

        }

        [HttpPut()]
        public async void OpdaterPerson(PersonDTO NyPerson)
        {
            repo.OpdaterPerson(NyPerson);

        }

    }
}