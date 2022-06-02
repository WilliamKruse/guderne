using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections;
using festivalprojekt.Server.Models;
using festivalprojekt.Shared.Models;


namespace festivalprojekt.Server.Controllers
{
    //Api routen til vagter tabellen i databasen
    [ApiController]
    [Route("api/festivalapi/vagter")]

    public class VagtController : ControllerBase
	{
        //Variabel
        private readonly IVagtRepositoryDapper repo;

        //contructor tjekker om VagtRepositoryDapper er tom og hvis den er, initialiseres repository
        public VagtController(IVagtRepositoryDapper vagtRepositoryDapper)
		{
            if ( vagtRepositoryDapper != null)
            {
                repo = vagtRepositoryDapper;
            }
		}

        //Implementer Get. Async metode som henter alle vagter fra databasen. Bruges i VagtService
        [HttpGet("hentallevagter")]
        public async Task<IEnumerable<VagtView>> HentAlleVagter(string streng, int id)
        {
            return await repo.HentAlleVagter(streng,id);
        }

        //Implementer Put. Async metode der booker en vagt og sætter det i databasen.Bruges i VagtService
        [HttpPut("bookvagt")]
        public async void BookVagt(BookVagtDTO Wrapper)
        {
            repo.BookVagt(Wrapper.VagtId, Wrapper.PersonId);
        }

        //Implementer Delete. Metode der sletter en vagt og sletter det i databasen. Bruges i VagtService
        [HttpDelete("{vagtid}")]
        public IActionResult SletVagt(int vagtid)
        {
            repo.SletVagt(vagtid);
            return Ok();
        }

        //Implementer Post. Metode der opretter en vagt og tilføjer denne vagt til databasen. Bruges i VagtSerive
        [HttpPost("opretvagt")]
        public void OpretVagt(VagtDTO NyVagt)
        {
            repo.OpretVagt(NyVagt);
        }
    }
}

