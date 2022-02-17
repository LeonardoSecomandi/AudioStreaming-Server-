using AudioStreaming.API.Models;
using AudioStreaming.API.Models.DTOS.Requests;
using AudioStreaming.API.Models.DTOS.Responses;
using AudioStreaming.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CanzoniController : ControllerBase
    {
        private ICanzoniService _canzoniService { get; set; }
        public CanzoniController(ICanzoniService canzoniService)
        {
            this._canzoniService = canzoniService;
        }



        /// <summary>
        /// Metodo di tipo POST utilizzato per aggiungere una canzone valida al database
        /// </summary>
        /// <param name="canzone">oggetto di tipo "canzone" che si vuole aggiungere</param>
        /// <returns>La canzone aggiunta se il modello era valido in caso contrario una BadRequest</returns>
        [HttpPost]
        public async Task<ActionResult<CreateCanzoneResponse>> AddCanzone(CreateCanzoneRequest canzone)
        {
            try
            {
                var result = await _canzoniService.AddCanzone(canzone);
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore nel recupero dati dal database");
            }

        }

        /// <summary>
        /// Meotodo che resituisce l'elenco di canzoni presenti sul database
        /// </summary>
        /// <returns>una lista di tipo canzone</returns>
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

        /// <summary>
        /// Metodo per la ricerca di una o più canzoni sul db per titolo della canzone o album di appartenenza
        /// </summary>
        /// <param name="req">oggetto contente due prametri
        /// Titolo:il titolo della canzone da cercare
        /// Album:l'album di appartenenza della canzone da cercare</param>
        /// <returns>una lista di canzoni che corrisponde ai risultati trovati</returns>
        [HttpPost("search")]
        public async Task<SearchSongResponse> Search(SearchSongRequest req)
        {
            var result = await _canzoniService.Search(req);
            return result;
        }

        /// <summary>
        /// metodo che dato l'id di una canzone restituisce la canzone con quell'id presente nel database,
        /// nel caso non esistesse non restituirà nulla.
        /// </summary>
        /// <param name="id">id della canzone che si desidera ottenere</param>
        /// <returns></returns>
        [HttpGet("canzone")]
        public async Task<ActionResult<Canzone>> GetCanzone(int id)
        {
            try
            {
                var result = await _canzoniService.GetCanzone(id);
                if (result != null)
                    return Ok(result);
                return NotFound("Nessuna canzone con id " + id + " trovata.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore nel recupero dati dal database");
            }
        }

        /// <summary>
        /// metodo utilizzato per eliminare una canzone 
        /// </summary>
        /// <param name="id">id della canzone che si vuole eliminare</param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<ActionResult<DeleteCanzoneResponse>> DeleteCanzone(int id)
        {
            try
            {
                var result = await _canzoniService.DeleteCanzone(id);
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore nel recupero dati dal database");
            }
            
        }

        /// <summary>
        /// Meotodo utilizzato per modificare una canzone
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<UpdateSongResponse>> UpdateCanzone(UpdateSongRequest req)
        {
            try
            {
                var result = await _canzoniService.UpdateCanzone(req);
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore nel recupero dati dal database");
            }
        }

        [HttpPost("download")]
        public async Task<ActionResult<ChangeDownloadNumberResp>> ChangeDownloadNumber(ChangeDownloadNumberReq req)
        {
            try
            {
                var result = await _canzoniService.ChangeDownloadNumber(req);
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Errore nel recupero dati dal database");
            }
        }
    }
}
