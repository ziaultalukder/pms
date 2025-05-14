using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Request.Configuration.Query;
using PMS.Application.Request.Sales.Command;
using PMS.Application.Request.Sales.Query;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> NewSales(AddNewSales command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult> GetClientWiseMedicineForSales(string medicineName)
        {
            var result = await _mediator.Send(new GetClientWiseMedicineForSales(medicineName));
            return Ok(result);
        }
    }
}
