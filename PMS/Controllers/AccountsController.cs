using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Request.Account.Command;
using PMS.Application.Request.Account.Query;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLogin command)
        {
            var data = await _mediator.Send(command);
            return Ok(data);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> UserRegistration(AddOrEditUser command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> ForgotPassword(ForgotPassword command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
