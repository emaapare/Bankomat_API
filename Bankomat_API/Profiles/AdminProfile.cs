using AutoMapper;
using Bankomat_API.Dto;
using Bankomat_API.Model;

namespace Bankomat_API.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Admin, AdminDto>().ReverseMap();
        }
    }
}
