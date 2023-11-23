//using AutoMapper;
//using Bankomat_API.Dto;
//using Bankomat_API.Model;

//namespace Bankomat_API.Profiles
//{
//    public class MappingProfile : Profile
//    {
//        public MappingProfile()
//        {
//            CreateMap<Banche, BancaDto>()
//            .ForMember(dest => dest.BancheFunzionalita, opt => opt.MapFrom(src => src.BancheFunzionalita.Select(f => f.IdFunzionalitaNavigation)));

//            CreateMap<Funzionalitum, FunzionalitumDto>();
//        }
//    }
//}
