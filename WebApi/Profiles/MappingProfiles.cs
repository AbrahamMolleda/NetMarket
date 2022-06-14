using AutoMapper;
using Core.Entities;
using WebApi.Dtos;

namespace WebApi.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Producto, ProductoDto>();
        }
    }
}
