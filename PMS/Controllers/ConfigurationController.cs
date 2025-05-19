using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualBasic;
using PMS.Application.Common.Pagins;
using PMS.Application.Request.Account.Command;
using PMS.Application.Request.Account.Query;
using PMS.Application.Request.Configuration.Command;
using PMS.Application.Request.Configuration.Query;
using PMS.ViewModel;
using System.Collections.Generic;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ConfigurationController : ControllerBase
    {
        private readonly IMediator _mediator;
        /*private readonly IMemoryCache _cache;*/
        public ConfigurationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> PopularMedicine()
        {
            /*var dd = _cache.Get("Data");
            if(dd != null)
            {
                return Ok(dd);
            }
            List<string> strings = new List<string>
            {
                "dddd",
                "sdfsdfsdf",
                "dsdsd"
            };
            _cache.Set("Data", strings);*/

            var result = await _mediator.Send(new PopularMedicine());
            return Ok(result);
        }
        
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDivision()
        {
            var result = await _mediator.Send(new GetDivision());
            return Ok(result);
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> MedicineList(string name, string getAll,int currentPage, int itemsPerPage)
        {
            var result = await _mediator.Send(new MedicineList(name, getAll,currentPage, itemsPerPage));
            PaginationHeader.Add(Response, result.CurrentPage, result.ItemsPerPage, result.TotalPages, result.TotalItems);
            return Ok(result); 
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> MedicineListByName(string name)
        {
            var result = await _mediator.Send(new MedicineListByName(name));
            return Ok(result); 
        }
        
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> MedicineListByNameForWeb(string name)
        {
            var result = await _mediator.Send(new MedicineListByName(name));
            return Ok(result); 
        }
        
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> MedicineListByNameForApp(string name)
        {
            var result = await _mediator.Send(new MedicineListByName(name));
            return Ok(result); 
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> ClientWiseMedicine(string medicineName, string getAll, int currentPage, int itemsPerPage)
        {
            var result = await _mediator.Send(new GetClientWiseMedicine(medicineName, getAll, currentPage, itemsPerPage));
            return Ok(result); 
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllClient(int id, string shopName, string contactNo, string getAll, int currentPage, int itemsPerPage)
        {
            var result = await _mediator.Send(new GetAllClient(id, shopName, contactNo, getAll, currentPage, itemsPerPage));
            PaginationHeader.Add(Response, result.CurrentPage, result.ItemsPerPage, result.TotalPages, result.TotalItems);
            return Ok(result);
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetSupplier(int id, string shopName, string getAll, int currentPage, int itemsPerPage)
        {
            var result = await _mediator.Send(new GetSupplier(id, shopName, getAll, currentPage, itemsPerPage));
            PaginationHeader.Add(Response, result.CurrentPage, result.ItemsPerPage, result.TotalPages, result.TotalItems);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _mediator.Send(new GetAdminProfile());
            return Ok(result);
        }


        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> AddClient(AddClient command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        
        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateClient(UpdateClient command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendToken(SendToken command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
