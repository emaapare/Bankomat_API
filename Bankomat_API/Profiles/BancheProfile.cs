﻿using AutoMapper;
using Bankomat_API.Dto;
using Bankomat_API.Model;

namespace Bankomat_API.Profiles
{
    public class BancheProfile : Profile
    {
        public BancheProfile()
        {
            CreateMap<Banche, BancaDto>().ReverseMap();
        }
    }

}
