using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        private readonly IMediator _mediator;
        /*private readonly IMemoryCache _cache;*/
        public HospitalController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
