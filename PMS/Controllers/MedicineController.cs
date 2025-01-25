using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMediator _mediator;
        /*private readonly IMemoryCache _cache;*/
        public MedicineController(IMediator mediator)
        {
            _mediator = mediator;
        }

    }
}
