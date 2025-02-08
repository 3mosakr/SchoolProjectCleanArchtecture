using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class ApplicationUserController : AppControllerBase
    {
        [HttpGet]
        [Route(Router.ApplicationUserRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetUserPaginationQuery query)
        {
            // send request to service to handle it
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route(Router.ApplicationUserRouting.GetById)]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            // send request to service to handle it
            var response = await Mediator.Send(new GetUserByIdQuery(id));
            return NewResult(response);
        }

        [HttpPost]
        [Route(Router.ApplicationUserRouting.Create)]
        public async Task<IActionResult> Create(AddUserCommand command)
        {
            // send request to service to handle it
            var response = await Mediator.Send(command);
            return NewResult(response);

        }

    }
}
