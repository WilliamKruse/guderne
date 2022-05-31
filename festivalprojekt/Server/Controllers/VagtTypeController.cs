using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections;
using festivalprojekt.Server.Models;
using festivalprojekt.Shared.Models;


namespace festivalprojekt.Server.Controllers
{
    //Api routen til vagt_typer tabellen i databasen
    [ApiController]
    [Route("api/festivalapi/vagttyper")]

    public class VagtTypeController : ControllerBase
	{
        //Attribut
        private readonly IVagtTypeReposityDapper repo;

        //contructor tjekker om VagtTypeRepositoryDapper er tom og hvis den er, initialiseres repository
        public VagtTypeController(IVagtTypeReposityDapper vagtTypeRepositoryDapper)
		{
            if (repo == null && vagtTypeRepositoryDapper != null)
            {
                repo = vagtTypeRepositoryDapper;
            }
        }

        //Implementer GET. Async metode som henter alle status fra databasen. Bruges i VagtTypeService
        [HttpGet("hentallestatus")]
        public async Task<IEnumerable<Status>> HentAlleStatus()
        {
            return await repo.HentAlleStatus();
        }

        //Implementer Get. Async metode som henter alle vagter typer fra databasen. Bruges i VagtTypeService
        [HttpGet("hentallevagttyper")]
        public async Task<IEnumerable<VagtTypeDTO>> HentAlleVagtTyper()
        {
            return await repo.HentAlleVagtTyper();
        }

        //Implementer Post. Metode der opretter en vagt type og tilføjer denne vagt type til databasen. Bruges i VagtTypeService
        [HttpPost("opretvagttype")]
        public async void OpretVagtType(VagtTypeDTO NyVagtType)
        {
            repo.OpretVagtType(NyVagtType);
        }

        //Implementer Delete. Metode der sletter en vagt type og sletter det i databasen. Bruges i VagtTypeService
        [HttpDelete("{vagttypeid}")]
        public IActionResult SletVagtType(int vagttypeid)
        {
            repo.SletVagtType(vagttypeid);
            return Ok();
        }

        //Implementer Put. Async metode der opdater en vagt type og sætter det i databasen.Bruges i VagtTypeService
        [HttpPut("opdatervagttype")]
        public async void OpdaterVagtType(VagtTypeDTO NyVagtType)
        {
            repo.OpdaterVagtType(NyVagtType);
        }
    }
}

