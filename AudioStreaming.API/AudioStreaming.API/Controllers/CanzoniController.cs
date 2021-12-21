using AudioStreaming.API.Models;
using AudioStreaming.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AudioStreaming.API.Controllers
{
    [ApiController]
    [Route("[Controller]/api")]
    public class CanzoniController : ControllerBase
    {
        private ICanzoniService _canzoniService { get; set; }
        public CanzoniController(ICanzoniService canzoniService)
        {
            this._canzoniService = canzoniService;
        }

        [HttpPost]
        public async Task<ActionResult<Canzone>> AddCanzone(Canzone canzone)
        {
            try
            {
                var result = await _canzoniService.AddCanzone(canzone);
                if (result != null)
                    return Ok("Canzone Aggiunta");
                return BadRequest("Dati non validi");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore nel recupero dati dal database");
            }
            
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Canzone>>> GetCanzoni()
        {
            try
            {
                var result = await _canzoniService.GetCanzoni();
                if (result != null)
                    return Ok(result);
                return NotFound("Nessuna Canzon Trovata");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore nel recupero dati dal database");
            }
            
        }
    }
}
