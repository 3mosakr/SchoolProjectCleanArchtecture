using Microsoft.AspNetCore.Mvc;
using SchoolProject.API.Base;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.API.Controllers
{
    [ApiController]
    public class DepartmentsController : AppControllerBase
    {


        [HttpGet]
        [Route(Router.DepartmentRouting.GetById)]
        public async Task<IActionResult> GetDepartmentByIdAsync([FromQuery] GetDepartmentByIdQuery query)
        {
            // send request to service to handle it
            var response = await Mediator.Send(query);
            return NewResult(response);
        }
    }
}
