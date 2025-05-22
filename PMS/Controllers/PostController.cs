using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Application.Common.Pagins;
using PMS.Application.Request.Post.Command;
using PMS.Application.Request.Post.Query;
using PMS.Application.Request.Sales.Query;

namespace PMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> GetPost(string title, int categoryId, int currentPage, int itemsPerPage)
        {
            var result = await _mediator.Send(new GetPost(title, categoryId, currentPage, itemsPerPage));
            PaginationHeader.Add(Response, result.CurrentPage, result.ItemsPerPage, result.TotalPages, result.TotalItems);
            return Ok(result);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult> CreatePost(CreatePost command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        
        [HttpPost("[action]")]
        public async Task<ActionResult> UpdatePost(UpdatePost command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
