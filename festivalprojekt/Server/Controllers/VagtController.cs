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
    [Route("api/festivalapi/vagter")]

    public class VagtController : ControllerBase
	{
        private readonly IVagtRepositoryDapper repo = new VagtRepositoryDapper();

		public VagtController(IVagtRepositoryDapper vagtRepositoryDapper)
		{
            if (repo == null && vagtRepositoryDapper != null)
            {
                repo = vagtRepositoryDapper;
                Console.WriteLine("Repository Initialized");
            }
		}

        [HttpGet("getvagter")]
        public async Task<IEnumerable<VagtDTO>> HentAlleVagter()
        {
            return await repo.HentAlleVagter();
        }

        [HttpPut("bookvagt")]
        public async void BookVagt(int VagtId, int PersonId)
        {
            repo.BookVagt(VagtId, PersonId);

        }

        [HttpDelete("sletvagt")]
        public async void SletVagt(int VagtId)
        {
            repo.SletVagt(VagtId);

        }

        [HttpPost("opretvagt")]
        public async void OpretVagt(VagtDTO NyVagt)
        {
            repo.OpretVagt(NyVagt);
        }


    }
}

