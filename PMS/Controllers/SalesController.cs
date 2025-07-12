using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Common.Pagins;
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
        
        [HttpPost("[action]")]
        public async Task<ActionResult> SalesRefund(SalesRefund command)
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
        public async Task<ActionResult> GetClientWiseMedicineForSales(string medicineName)
        {
            var result = await _mediator.Send(new GetClientWiseMedicineForSales(medicineName));
            return Ok(result);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult> GetSales(string startDate, string endDate, int currentPage, int itemsPerPage)
        {
            var result = await _mediator.Send(new GetSales(startDate, endDate, currentPage, itemsPerPage));
            PaginationHeader.Add(Response, result.CurrentPage, result.ItemsPerPage, result.TotalPages, result.TotalItems);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> SalesReport(string startDate, string endDate, int currentPage, int itemsPerPage)
        {
            var result = await _mediator.Send(new SalesReport(startDate, endDate, currentPage, itemsPerPage));
            PaginationHeader.Add(Response, result.CurrentPage, result.ItemsPerPage, result.TotalPages, result.TotalItems);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetSalesInfoForRefund(string invoiceNo)
        {
            var result = await _mediator.Send(new GetSalesInfoForRefund(invoiceNo));
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetSalesDetailsById(int id)
        {
            var result = await _mediator.Send(new GetSalesDetailsById(id));
            return Ok(result);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult> TodayMonthlyAndYearlySalesReport()
        {
            var result = await _mediator.Send(new TodayMonthlyAndYearlySalesReport());
            return Ok(result);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult> WeeklyChartSalesReport()
        {
            var result = await _mediator.Send(new WeeklyChartSalesReport());
            return Ok(result);
        }
        
        [HttpGet("[action]")]
        public async Task<ActionResult> WeeklyTopSalesMedicineReport(int value)
        {
            var result = await _mediator.Send(new WeeklyTopSalesMedicineReport(value));
            return Ok(result);
        }
    }
}
