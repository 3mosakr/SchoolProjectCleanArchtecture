using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler,
        IRequestHandler<AddStudentCommand, Response<string>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public StudentCommandHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        #endregion

        #region Handle Methods
        #endregion
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            // mapping Between request and student
            var studentMapper = _mapper.Map<Student>(request);
            // add
            var result = await _studentService.AddAsync(studentMapper);
            // check condtion
            if (result == "Exist")
                return UnprocessableEntity<string>("Name is Exist");
            // return response
            else if (result == "Success")
                return Created("Added Successfully");
            else if (result == "Fail")
                return BadRequest<string>($"there is no department with this Id {request.DepartmentId}");
            else
                return BadRequest<string>();
        }
    }
}
