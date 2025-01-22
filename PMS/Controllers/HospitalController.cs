using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Request.Hospital.Query;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HospitalController : ControllerBase
    {
        private readonly IMediator _mediator;
        public HospitalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPopularHospital()
        {
            var result = await _mediator.Send(new PopularHospital());
            return Ok(result);
        }
        
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDivisionAndDistrictWiseHospital(int divisionId)
        {
            var result = await _mediator.Send(new GetDivisionAndDistrictWiseHospital(divisionId));
            return Ok(result);
        }
        
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetHospitalById(int id)
        {
            var result = await _mediator.Send(new GetHospitalById(id));
            return Ok(result);
        }
    }
}
