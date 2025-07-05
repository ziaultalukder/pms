using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Common.Pagins;
using PMS.Application.Request.Account.Command;
using PMS.Application.Request.Sales.Command;
using PMS.Application.Request.Sales.Query;
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
        
        [HttpGet("[action]")]
        public async Task<ActionResult> GetStock(string startDate, string endDate, int currentPage, int itemsPerPage)
        {
            var result = await _mediator.Send(new GetStock(startDate, endDate, currentPage, itemsPerPage));
            PaginationHeader.Add(Response, result.CurrentPage, result.ItemsPerPage, result.TotalPages, result.TotalItems);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetStockInfoForRefund(string invoiceNo)
        {
            var result = await _mediator.Send(new GetStockInfoForRefund(invoiceNo));
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> StockRefund(StockRefund command)
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
    }
}
