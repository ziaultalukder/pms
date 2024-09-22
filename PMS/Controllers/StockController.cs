using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Request.Account.Command;
using PMS.Application.Request.Stock.Command;
using PMS.Application.Request.Stock.Query;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> NewStock(AddNewStock command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> GetStock(GetStock command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
