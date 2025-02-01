using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class ApplicationUserController : AppControllerBase
    {
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
