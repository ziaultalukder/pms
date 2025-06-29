using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Request.Account.Command;
using PMS.Application.Request.Account.Query;
using PMS.ViewModel;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost("[action]")]
        [AllowAnonymous]
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
        public async Task<IActionResult> ActiveAndDeActiveUser(ActiveAndDeActiveUser command)
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

        [HttpPost("[action]")]
        public async Task<IActionResult> ChangePassword(ChangePassword command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        
        /*[HttpGet("[action]")]
        public async Task<IActionResult> GetClientUsers()
        {
            var result = await _mediator.Send(new GetClientUsers());
            return Ok(result);
        }*/

    }
}
