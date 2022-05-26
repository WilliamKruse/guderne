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
        private readonly IVagtRepositoryDapper repo;

		public VagtController(IVagtRepositoryDapper vagtRepositoryDapper)
		{
            if ( vagtRepositoryDapper != null)
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
        public async void BookVagt(BookVagtDTO Wrapper)
        {
            repo.BookVagt(Wrapper.VagtId, Wrapper.PersonId);

        }

        [HttpDelete("{vagtid}")]
        public IActionResult SletVagt(int vagtid)
        {
            repo.SletVagt(vagtid);
            return Ok();

        }

        [HttpPost("opretvagt")]
        public void OpretVagt(VagtDTO NyVagt)
        {
            repo.OpretVagt(NyVagt);
        }


    }
}

