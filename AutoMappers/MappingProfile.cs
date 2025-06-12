using async.dto;
using async.Models;
using AutoMapper;

namespace async.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BeerInsertDto, Beer>();
            //Mapeo cuando las clases tienen campos con nombres que no coinciden
            CreateMap<Beer, BeerDto>()
                .ForMember(dto => dto.Id, b => b.MapFrom(b => b.BeerId));
            //Mapeo de un objeto ya existente
            CreateMap<BeerUpdateDto, Beer>();
        }
    }
}
