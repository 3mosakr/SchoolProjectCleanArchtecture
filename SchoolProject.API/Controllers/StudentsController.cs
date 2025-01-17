using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class StudentsController : AppControllerBase
    {

        [HttpGet]
        [Route(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentListAsync()
        {
            // send request to service to handle it
            var response = await Mediator.Send(new GetStudentListQuery());
            return Ok(response);
        }

        [HttpGet]
        [Route(Router.StudentRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetStudentPaginatedListQuery query)
        {
            // send request to service to handle it
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        [Route(Router.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudentByIdAsync(int id)
        {
            // send request to service to handle it
            var response = await Mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(response);
        }

        [HttpPost]
        [Route(Router.StudentRouting.Create)]
        public async Task<IActionResult> Create(AddStudentCommand command)
        {
            // send request to service to handle it
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpPut]
        [Route(Router.StudentRouting.Edit)]
        public async Task<IActionResult> Edit(EditStudentCommand command)
        {
            // send request to service to handle it
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpDelete]
        [Route(Router.StudentRouting.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            // send request to service to handle it
            var response = await Mediator.Send(new DeleteStudentCommand(id));
            return NewResult(response);
        }
    }
}
