using AutoMapper;
using card_api.DTOs;
using card_api.Models;
using System;

namespace card_api.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Card, CardDto>().ForMember(dest => dest.Image, opts => 
                opts.MapFrom(src => Convert.FromBase64String(src.ImageBase64String)));

            CreateMap<CardDto, Card>().ForMember(dest => dest.ImageBase64String, opts =>
                opts.MapFrom(src => Convert.ToBase64String(src.Image)));
        }
    }
}
