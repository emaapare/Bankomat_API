using Bankomat_API.Dto;
using Bankomat_API.Model;
using System.Threading.Tasks;

namespace Bankomat_API.Repositories
{
    public interface IDbRepository
    {
        Task<IEnumerable<Utenti>> GetUtentiAsync();
        Task<Utenti> GetUtenteAsync(long id);
        Task AddUtenteAsync(UtenteDto utente);
        Task<bool> IsNomeUtenteExistsAsync(string nomeUtente);
        Task<bool> IsBancaValidAsync(long idBanca);
        Task<bool> IsUtenteBloccatoAsync(long idBanca);
        Task UpdateUtentePasswordAsync(long id, string password);
        Task UpdateUtenteBloccatoAsync(long id, bool bloccato);
        Task DeleteUtenteAsync(Utenti utente);
        Task<IEnumerable<Banche>> GetBancheAsync();
        Task <Banche> GetBancaAsync(long id);
        Task<Utenti> DoLoginAsync(string nomeUtente, string password);
        Task<IEnumerable<Funzionalitum>> GetFunzionalitasBancaAsync(long idBanca);
        Task<IEnumerable<Funzionalitum>> GetFunzionalitaAsync();
        Task AttivaFunzionalitaBancaAsync(int bancaId, int functionalityId);

        Task DisattivaFunzionalitaBancaAsync(int bancaId, int functionalityId);
    }
}
