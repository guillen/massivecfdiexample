using AutoMapper;
using Cfdi.Domain.DTOs.Request;
using Cfdi.Domain.Models;
using Cfdi.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cfdi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CfdiController : ControllerBase
    {
        private readonly IQueueService _queueService;
        private readonly IMapper _mapper;

        public CfdiController(IQueueService queueService, IMapper mapper)
        {
            _queueService = queueService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ICollection<Queue>> Get()
        {
            return await _queueService.AllAsync();
        }

        [HttpPost("query")]
        public async Task<ActionResult<Queue>> Query(CfdiRequest request)
        {
            Queue queue = _mapper.Map<Queue>(request);
            return Ok(await _queueService.AddQueue(queue));
        }
    }
}
