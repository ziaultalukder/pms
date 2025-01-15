using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Common.Pagins;
using PMS.Application.Request.Category.Query;
using PMS.Application.Request.Configuration.Query;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        /*private readonly IMemoryCache _cache;*/
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> MedicalDepartmentName()
        {
            var result = await _mediator.Send(new GetMedicalDepartmentName());
            return Ok(result);
        }
    }
}
