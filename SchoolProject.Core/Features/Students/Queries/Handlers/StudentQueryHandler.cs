using AutoMapper;
using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler : ResponseHandler,
        IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
        IRequestHandler<GetStudentByIdQuery, Response<GetSingleStudentResponse>>,
        IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentQueryHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }
        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            // Mapping using AutoMapper
            var studentList = await _studentService.GetStudentsListAsync();
            var studentListMapper = _mapper.Map<List<GetStudentListResponse>>(studentList);


            return Success(studentListMapper);
        }

        public async Task<Response<GetSingleStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdWithIncludeAsync(request.Id);
            if (student == null)
                return NotFound<GetSingleStudentResponse>($"There is no Id = {request.Id}");

            var result = _mapper.Map<GetSingleStudentResponse>(student);
            return Success(result);

        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            // Make Expression to get students and put it into response 
            // - convert from student to GetStudentPaginatedListResponse
            Expression<Func<Student, GetStudentPaginatedListResponse>> expression =
                student => new GetStudentPaginatedListResponse(student.Id, student.NameEn, student.Address, student.Department.NameEn);

            var FilterQuery = _studentService.FilterStudentPaginatedQuerable(request.OrderBy, request.Search);

            var paginatedList = await FilterQuery
            .Select(expression) // convert from student to GetStudentPaginatedListResponse
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return paginatedList;

        }
    }
}
