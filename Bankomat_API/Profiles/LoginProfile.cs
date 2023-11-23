using AutoMapper;
using Bankomat_API.Dto;
using Bankomat_API.Model;

namespace Bankomat_API.Profiles
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<LoginRequestDto, Utenti>().ReverseMap();
        }
    }
}
