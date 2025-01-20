using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        /*private readonly IMemoryCache _cache;*/
        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
