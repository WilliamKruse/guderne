using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections;
using festivalprojekt.Server.Models;
using festivalprojekt.Shared.Models;

namespace festivalprojekt.Server.Controllers
{
    //Api routen til personer tabellen i databasen
    [ApiController]
    [Route("api/festivalapi/personer")]

    public class PersonController : ControllerBase
    {
        //Variable
        private readonly IPersonRepositoryDapper repo;

        //Contructor tjekker om PersonRepositoryDapper er tom og hvis den er, initialiseres repository
        public PersonController(IPersonRepositoryDapper personRepositoryDapper)
        {
            if (personRepositoryDapper != null)
            {
                repo = personRepositoryDapper;
            }
        }

        //Implementer GET. Async metode som henter alle rollerne fra databasen. Bruges i PersonService
        [HttpGet("hentalleroller")]
        public async Task<IEnumerable<Roller>> HentAlleRoller()
        {
            return await repo.HentAlleRoller();
        }

        //Implementer Get. Async metoden henter alle personer fra databasen. Bruges i PersonService
        [HttpGet("hentallekompetencer")]
        public async Task<IEnumerable<Kompetencer>> HentAlleKompetencer()
        {
            return await repo.HentAlleKompetencer();
        }

        //Implementer Get. Async metoden henter person på personid fra databasen. Bruges i PersonService
        [HttpGet("hentallepersoner")]
        public async Task<IEnumerable<PersonDTO>> HentAllePersoner()
        {
            return await repo.HentAllePersoner();
        }

        //Implementer Get. Async metoden henter person på personid fra databasen. Bruges i PersonService
        [HttpGet("hentperson")]
        public async Task<IEnumerable<PersonDTO>> HentPerson(int personid)
        {
            return await repo.HentPerson(personid);
        }

        //Implementer Post. Async metode der opretter en person og sætter personen i databasen. Bruges i PersonService
        [HttpPost("opret")]
        public async void OpretPerson(PersonDTO NyPerson)
        {
            repo.OpretPerson(NyPerson);
        }

        //Implementer Put. Async metode der opdater en person og sætter det i databasen. Bruges i PersonService
        [HttpPut("opdaterperson")]
        public async void OpdaterPerson(PersonDTO NyPerson)
        {
            repo.OpdaterPerson(NyPerson);
        }

        //Implementer Get. Async metode der logger en person ind. Bruges i LoginService.
        [HttpGet("login")]
        public async Task<IEnumerable<PersonDTO>> Login(string email, string kode)
        {
            return await repo.Login(email, kode); 
        }
    }
}