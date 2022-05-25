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
        private readonly IPersonRepositoryDapper repo;

        public PersonController(IPersonRepositoryDapper personRepositoryDapper)
        {
            if (personRepositoryDapper != null)
            {
                repo = personRepositoryDapper;
                Console.WriteLine("Repository Initialized");
            }
        }

        [HttpGet("hentalleroller")]
        public async Task<IEnumerable<Roller>> HentAlleRoller()
        {
            return await repo.HentAlleRoller();
        }


        [HttpGet("hentallekompetencer")]
        public async Task<IEnumerable<Kompetencer>> HentAlleKompetencer()
        {
            return await repo.HentAlleKompetencer();
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

        //api/festivalapi/personer/opret

        [HttpPost("opret")]
        public async void OpretPerson(PersonDTO NyPerson)
        {
            Console.WriteLine("controller fandt opret");
            repo.OpretPerson(NyPerson);

        }

        [HttpPut("opdaterperson")]
        public async void OpdaterPerson(PersonDTO NyPerson)
        {
            repo.OpdaterPerson(NyPerson);

        }
        [HttpGet("login")]
        public async Task<IEnumerable<PersonDTO>> Login(string email, string kode)
        {
            Console.WriteLine($"login controller{email}{kode}");
            return (await repo.Login(email, kode)).ToList();
        }
    }
}