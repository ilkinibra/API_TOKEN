using ApiPractice.Data.Entities;
using ApiPractice.Dto;
using AutoMapper;

namespace ApiPractice.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Product, ProductReturnDto>()
                .ForMember(ds => ds.FullImageUrl, map => map.MapFrom(sr => "https://localhost:44358/img/"+sr.ImageUrl));
        }
    }
}
