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

        [HttpGet("hentallevagter")]
        public async Task<IEnumerable<VagtView>> HentAlleVagter(string streng, int id)
        {
            return await repo.HentAlleVagter(streng,id);
        }

        [HttpPut("bookvagt")]
        public async void BookVagt(int VagtId, int PersonId)
        {
            repo.BookVagt(VagtId, PersonId);

        }

        [HttpDelete("{vagtid}")]
        public IActionResult SletVagt(int vagtid)
        {
            repo.SletVagt(vagtid);
            return Ok();

        }

        [HttpPost("opretvagt")]
        public async void OpretVagt(VagtDTO NyVagt)
        {
            repo.OpretVagt(NyVagt);
        }


    }
}

