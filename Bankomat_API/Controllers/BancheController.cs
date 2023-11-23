using AutoMapper;
using Bankomat_API.Dto;
using Bankomat_API.Model;
using Bankomat_API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bankomat_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BancheController : ControllerBase
    {
        private readonly IDbRepository _repo;
        private readonly IMapper _mapper;

        public BancheController(IDbRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BancaDto>>> GetBancheAsync()
        {
            var res = _mapper.Map<List<BancaDto>>(await _repo.GetBancheAsync());
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto loginRequest)
        {
            var utenteAutenticato = await _repo.DoLoginAsync(loginRequest.Username, loginRequest.Password);

            if (utenteAutenticato != null)
            {
                var utenteDto = _mapper.Map<UtenteDto>(utenteAutenticato);
                return Ok(new LoginResponseDto { Success = true, Utente = utenteDto });
            }
            else
            {
                return NotFound(new LoginResponseDto { Success = false, Message = "Utente non esistente o password errata" });
            }
        }

        [HttpGet("funzionalita/{bancaId}")]
        public async Task<ActionResult<IEnumerable<BancheFunzionalitum>>> GetFunzionalitasBancaByIdAsync(int bancaId)
        {

            var funzionalitas = await _repo.GetFunzionalitasBancaAsync(bancaId);
            if (funzionalitas == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<Funzionalitum>, IEnumerable<FunzionalitumDto>>(funzionalitas));
        }

        [HttpGet("funzionalita")]
        public async Task<ActionResult<IEnumerable<BancheFunzionalitum>>> GetFunzionalitaAsync()
        {

            var funzionalitas = await _repo.GetFunzionalitaAsync();
            if (funzionalitas == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<Funzionalitum>, IEnumerable<FunzionalitumDto>>(funzionalitas));
        }

        [HttpPost("attiva/{bankId}/{functionalityId}")]
        public async Task<IActionResult> AttivaFunzionalita(int bankId, int functionalityId)
        {
            try
            {
                await _repo.AttivaFunzionalitaBancaAsync(bankId, functionalityId);
                return Ok("Funzionalità attivata con successo.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore durante l'attivazione della funzionalità: {ex.Message}");
            }
        }

        [HttpDelete("disattiva/{bankId}/{functionalityId}")]
        public async Task<IActionResult> DisattivaFunzionalita(int bankId, int functionalityId)
        {
            try
            {
                await _repo.DisattivaFunzionalitaBancaAsync(bankId, functionalityId);
                return Ok("Funzionalità disattivata con successo.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore durante la disattivazione della funzionalità: {ex.Message}");
            }
        }
    }
}
