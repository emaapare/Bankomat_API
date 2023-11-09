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
            var banche = await _repo.GetBancheAsync();

            var bancheDto = banche.Select(b =>
            {
                var bancaDto = _mapper.Map<BancaDto>(b);
                bancaDto.BancheFunzionalita = _mapper.Map<List<FunzionalitumDto>>(
                    b.BancheFunzionalita.Select(f => f.IdFunzionalitaNavigation).ToList()
                );
                return bancaDto;
            }).ToList();

            return Ok(bancheDto);
        }

    }
}
