using AutoMapper;
using Bankomat_API.Dto;
using Bankomat_API.Model;

namespace Bankomat_API.Profiles
{
    public class UtentiProfile : Profile
    {
        public UtentiProfile()
        {
            CreateMap<Utenti, UtenteDto>().ReverseMap();
        }
    }
}
