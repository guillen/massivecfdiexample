using AutoMapper;
using Cfdi.Domain.DTOs.Request;
using Cfdi.Domain.Models;
using Newtonsoft.Json;

namespace Cfdi.API.Mapper
{
    public class CfdiProfile : Profile
    {
        public CfdiProfile()
        {
            CreateMap<CfdiRequest, Queue>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src)));
        }
    }
}
