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
        public IEnumerable<PersonDTO> HentAllePersoner() 
        {
            return repo.HentAllePersoner();
        }
    }
}