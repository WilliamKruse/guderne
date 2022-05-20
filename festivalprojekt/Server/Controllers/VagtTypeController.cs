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
    [Route("api/festivalapi/vagttyper")]

    public class VagtTypeController : ControllerBase

	{
        private readonly IVagtTypeReposityDapper repo = new VagtTypeRepositoryDapper();

		public VagtTypeController(IVagtTypeReposityDapper vagtTypeRepositoryDapper)
		{
            if (repo == null && vagtTypeRepositoryDapper != null)
            {
                repo = vagtTypeRepositoryDapper;
                Console.WriteLine("Repository Initialized");
            }
        }

        [HttpGet("hentallevagttyper")]
        public async Task<IEnumerable<VagtTypeDTO>> HentAlleVagtTyper()
        {
            return await repo.HentAlleVagtTyper();
        }

        [HttpPost("opretvagttype")]
        public async void OpretVagtType(VagtTypeDTO NyVagtType)
        {
            repo.OpretVagtType(NyVagtType);
        }


        [HttpDelete("sletvagttype")]
        public async void SletVagtType(int VagtTypeId)
        {
            repo.SletVagtType(VagtTypeId);

        }


    }
}

