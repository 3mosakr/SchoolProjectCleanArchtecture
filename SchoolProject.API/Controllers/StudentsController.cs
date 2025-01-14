using MediatR;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudentListAsync()
        {
            // send request to service to handle it
            var response = await _mediator.Send(new GetStudentListQuery());
            return Ok(response);
        }

        [HttpGet]
        [Route(Router.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudentByIdAsync(int id)
        {
            // send request to service to handle it
            var response = await _mediator.Send(new GetStudentByIdQuery(id));
            return Ok(response);
        }

        [HttpPost]
        [Route(Router.StudentRouting.Create)]
        public async Task<IActionResult> Create(AddStudentCommand command)
        {
            // send request to service to handle it
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
