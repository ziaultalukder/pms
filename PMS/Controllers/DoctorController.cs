using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Request.Category.Query;
using PMS.Application.Request.Doctor.Command;
using PMS.Application.Request.Doctor.Query;
using PMS.Models;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DoctorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddDoctor(AddDoctor command)
        {
            var result = await _mediator.Send(command);
            if (result.Succeed)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(StatusCodes.Status400BadRequest);
            }
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> EditDoctor(UpdateDoctor command)
        {
            var result = await _mediator.Send(command);
            if(result.Succeed)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(StatusCodes.Status400BadRequest);
            }
            
        }

        
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDoctorCategoryId(int categoryId)
        {
            var result = await _mediator.Send(new GetDoctorCategoryId(categoryId));
            return Ok(result);
        }

        
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPopularDoctor()
        {
            var result = await _mediator.Send(new GetPopularDoctor());
            return Ok(result);
        }
    }
}
