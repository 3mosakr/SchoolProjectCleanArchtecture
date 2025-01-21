using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler,
        IRequestHandler<AddStudentCommand, Response<string>>,
        IRequestHandler<EditStudentCommand, Response<string>>,
        IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructor
        public StudentCommandHandler(IStudentService studentService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion

        #region Handle Methods

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            // mapping Between request and student
            var studentMapper = _mapper.Map<Student>(request);
            // add
            var result = await _studentService.AddAsync(studentMapper);

            // return response
            if (result == "Success")
                return Created("");
            else
                return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            // check if id is exist or not
            var studentExist = await _studentService.GetStudentByIdWithIncludeAsync(request.Id);
            if (studentExist == null)
                return NotFound<string>($"There is no student with this id {request.Id}");
            // mapping Between request and student
            var studentMapper = _mapper.Map(request, studentExist);
            // make edit ( call service )
            var result = await _studentService.EditAsync(studentMapper);

            if (result == "Success")
                return Success($"Student With Id {request.Id} is " + (string)_localizer[SharedResourcesKeys.Updated]);
            else
                return BadRequest<string>();

        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            // check if id is exist or not
            var studentExist = await _studentService.GetByIdAsync(request.Id);
            if (studentExist == null)
                return NotFound<string>($"There is no student with this id {request.Id}");

            // make Delete ( call service )
            var result = await _studentService.DeleteAsync(studentExist);

            if (result == "Success")
                return Deleted<string>($"Delete Successfully at student with id = {studentExist.Id}");
            else
                return BadRequest<string>();
        }


        #endregion
    }
}
