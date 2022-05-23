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

        [HttpGet("hentallepersoner")]
        public async Task<IEnumerable<PersonDTO>> HentAllePersoner()
        {
            return await repo.HentAllePersoner();
        }
        [HttpGet("hentperson")]
        public async Task<IEnumerable<PersonDTO>> HentPerson(int personid)
        {
            return await repo.HentPerson(personid);
        }

        [HttpPost("opretperson")]
        public async void OpretPerson(PersonDTO NyPerson)
        {
            repo.OpretPerson(NyPerson);

        }

        [HttpPut("opdaterperson")]
        public async void OpdaterPerson(PersonDTO NyPerson)
        {
            repo.OpdaterPerson(NyPerson);

        }

    }
}