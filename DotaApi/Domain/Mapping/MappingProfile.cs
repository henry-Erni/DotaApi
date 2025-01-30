using AutoMapper;
using DotaApi.Domain.Contracts;
using DotaApi.Domain.Entities;

namespace DotaApi.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Hero, GetHeroDTO>();
            CreateMap<CreateHero, Hero>();
            CreateMap<UpdateHero, Hero>();
            CreateMap<DeleteHero, Hero>();
            //CreateMap<GetHero, Hero>();



            CreateMap<Item, GetItemDTO>();
            CreateMap<CreateItem, Item>();
            CreateMap<UpdateItem, Item>();
            CreateMap<DeleteItem, Item>();
            //CreateMap<GetItem, Item>();
        }
    }
}
