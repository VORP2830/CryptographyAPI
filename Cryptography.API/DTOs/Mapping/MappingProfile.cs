using AutoMapper;
using Cryptography.API.Models;

namespace Cryptography.API.DTOs.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SensitiveData, SensitiveDataDTO>().ReverseMap();
        }
    }
}