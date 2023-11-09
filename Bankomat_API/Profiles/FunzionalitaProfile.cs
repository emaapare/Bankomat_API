using AutoMapper;
using Bankomat_API.Dto;
using Bankomat_API.Model;
using System.Runtime;

namespace Bankomat_API.Profiles
{
    public class FunzionalitaProfile : Profile
    {
        public FunzionalitaProfile()
        {
            CreateMap<Funzionalitum, FunzionalitumDto>().ReverseMap();
        }
    }

}
