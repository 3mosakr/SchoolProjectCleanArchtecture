using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Department.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler,
        IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalization;
        #endregion

        #region Constructor
        public DepartmentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            IDepartmentService departmentService,
            IStudentService studentService) : base(stringLocalizer)
        {
            _stringLocalization = stringLocalizer;
            _mapper = mapper;
            _departmentService = departmentService;
            _studentService = studentService;
        }
        #endregion

        #region Handle Methods

        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            // Service Get By Id include student, subject and instructor
            var department = await _departmentService.GetDepartmentByIdAsync(request.Id);
            // Check is not exist
            if (department == null)
                return NotFound<GetDepartmentByIdResponse>(_stringLocalization[SharedResourcesKeys.NotFound]);
            // mapping 
            var mapper = _mapper.Map<GetDepartmentByIdResponse>(department);
            // pagination
            Expression<Func<Student, StudentResponse>> expression =
                student => new StudentResponse(
                    student.Id,
                    student.Localize(student.NameAr, student.NameEn)
                   );
            var studentQuerable = _studentService.GetStudentsByDepartmentIdQueryable(request.Id);

            var paginatedList = await studentQuerable
            .Select(expression) // convert from student to GetStudentPaginatedListResponse
            .ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);

            mapper.StudentList = paginatedList;
            // return response
            return Success(mapper);

        }
        #endregion
    }
}
