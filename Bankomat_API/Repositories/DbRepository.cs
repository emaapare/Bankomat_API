using AutoMapper;
using Bankomat_API.Context;
using Bankomat_API.Dto;
using Bankomat_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Bankomat_API.Repositories
{
    public class DbRepository : IDbRepository
    {
        private SoluzioneBankomatContext _context;
        private readonly IMapper _mapper;

        public DbRepository(SoluzioneBankomatContext ctx, IMapper mapper)
        {
            _context = ctx ?? throw new NotImplementedException(nameof(ctx));
            _mapper = mapper ?? throw new NotImplementedException(nameof(mapper));
        }

        public async Task<IEnumerable<Utenti>> GetUtentiAsync()
        {
            var utenti = await _context.Utentis.ToListAsync();
            return utenti;
        }

        public async Task<Utenti> GetUtenteAsync(long id)
        {
            return await _context.Utentis.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> IsNomeUtenteExistsAsync(string nomeUtente)
        {
            return await _context.Utentis.AnyAsync(u => u.NomeUtente == nomeUtente);
        }

        public async Task<bool> IsBancaValidAsync(long idBanca)
        {
            return await _context.Banches.AnyAsync(b => b.Id == idBanca && idBanca > 0);
        }

        public async Task AddUtenteAsync(UtenteDto utenteDto)
        {
            if (utenteDto == null)
            {
                throw new ArgumentNullException(nameof(utenteDto));
            }

            if(utenteDto.IdBanca < 0 || utenteDto.IdBanca > _context.Banches.Count())
            {
                throw new ArgumentNullException(nameof(utenteDto.IdBanca));
            }

            var nuovoUtente = _mapper.Map<Utenti>(utenteDto);

            var cc = new ContiCorrente()
            {
                IdUtente = nuovoUtente.Id,
                Saldo = 0,
                DataUltimaOperazione = DateTime.Today,
                DataUltimoVersamento = DateTime.Today,
            };

            nuovoUtente.ContiCorrentes.Add(cc);

            
            _context.Utentis.Add(nuovoUtente);
            await _context.SaveChangesAsync();
            
        }

        public async Task<bool> IsUtenteBloccatoAsync(long id)
        {
            return await _context.Utentis.AnyAsync(u => u.Id == id && u.Bloccato);
        }

        public async Task UpdateUtentePasswordAsync(long utenteId, string password)
        {
            var ut = await _context.Utentis.FindAsync(utenteId);

            if (ut != null)
            {
                ut.Password = password;
                
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUtenteBloccatoAsync(long utenteId, bool bloccato)
        {
            var ut = await _context.Utentis.FindAsync(utenteId);

            if(ut != null)
            {
                ut.Bloccato = bloccato;
                
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUtenteAsync(Utenti utente)
        {
            if (utente != null)
            {
                _context.Utentis.Remove(utente);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Banche>> GetBancheAsync()
        {
            var banches = await _context.Banches.ToListAsync();
            return banches;
        }

        public Task<Banche> GetBancaAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BancheFunzionalitum>> GetFunzionalitaBancaAsync(int bancaId)
        {
            return await _context.BancheFunzionalita
                .Where(f => f.IdBanca == bancaId)
                .ToListAsync();
        }

        public Task<Funzionalitum> GetFunzionalitaBancaAsync()
        {
            throw new NotImplementedException();
        }
    }
}
