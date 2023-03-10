using AutoMapper;
using Cfdi.Domain.DTOs.Request;
using Cfdi.Domain.Models;
using Newtonsoft.Json;

namespace Cfdi.Worker.Mapper
{
    internal class WorkerProfile : Profile
    {
        private CfdiHistory _history;

        public WorkerProfile()
        {
            CreateMap<Queue, CfdiRequest>()
                .ForMember(dest => dest.Endoso, opt => opt.MapFrom(src => resolveData(src.Data, "Endoso")))
                .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => resolveData(src.Data, "Usuario")))
                .AfterMap((queue, cfdiHistory) => _history = null);
        }

        public string resolveData(string data, string property)
        {
            if (_history == null)
            {
                _history = JsonConvert.DeserializeObject<CfdiHistory>(data);
            }

            var value = _history.GetType().GetProperty(property).GetValue(_history);

            return value == null ? "" : value.ToString();
        }
    }
}
