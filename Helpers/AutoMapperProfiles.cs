

using System.Collections.Generic;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<NewYork, NewYorkDtos>();
            CreateMap<NewYork, NewYorkSecondDtos>();
            CreateMap<NewYorkSecondDtos, NewYork>();
            // CreateMap<NewYork, NewYorkSecondDtos>();
        }

    }
}