using AutoMapper;
using Bankomat_API.Dto;
using Bankomat_API.Model;
using Bankomat_API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bankomat_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtentiController : ControllerBase
    {
        private readonly IDbRepository _repo;
        private readonly IMapper _mapper;

        public UtentiController(IDbRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utenti>>> GetUtentiAsync()
        {
            var res = _mapper.Map<List<UtenteDto>>(await _repo.GetUtentiAsync());
            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Utenti>>> GetUtenteAsync(int id)
        {
            var ut = await _repo.GetUtentiAsync();
            var utente = ut.FirstOrDefault(c => c.Id == id);

            if (utente == null)
            {
                return NotFound("utente non trovato");
            }

            var res = _mapper.Map<UtenteDto>(utente);

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddUtenteAsync([FromBody] UtenteDto utente)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _repo.IsBancaValidAsync(utente.IdBanca))
            {
                return BadRequest("la banca specificata non è valida");
            }

            if (await _repo.IsNomeUtenteExistsAsync(utente.NomeUtente))
            {
                return BadRequest("il nome utente esiste già, scegliere un nome utente diverso");
            }

            //var nuovoUtente = _mapper.Map<UtenteDto>(utente);

            await _repo.AddUtenteAsync(utente);

            return Ok("utente aggiunto con successo");
        }

        [HttpPut("password/{id}")]
        public async Task<IActionResult> PutUtentePasswordAsync(long id, string password)
        {
            var ut = await _repo.GetUtenteAsync(id);

            if (await _repo.IsUtenteBloccatoAsync(id))
            {
                return BadRequest("l'utente è bloccato, impossibile modificare la password");
            }

            if (ut == null)
            {
                return NotFound("utente non trovato");
            }

            await _repo.UpdateUtentePasswordAsync(id, password);

            return Ok("utente aggiornato con successo");
        }

        [HttpPut("bloccato/{id}")]
        public async Task<IActionResult> PutUtenteBloccatoAsync(long id, bool bloccato)
        {
            var ut = await _repo.GetUtenteAsync(id);

            if (ut == null)
            {
                return NotFound("utente non trovato");
            }

            await _repo.UpdateUtenteBloccatoAsync(id, bloccato);

            return Ok("utente aggiornato con successo");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtenteAsync(int id)
        {
            var ut = await _repo.GetUtentiAsync();
            var utente = ut.FirstOrDefault(c => c.Id == id);

            if (utente == null)
            {
                return NotFound("utente non trovato");
            }

            await _repo.DeleteUtenteAsync(utente);

            var res = _mapper.Map<List<UtenteDto>>(await _repo.GetUtentiAsync());

            return Ok(res);
        }
    }
}
