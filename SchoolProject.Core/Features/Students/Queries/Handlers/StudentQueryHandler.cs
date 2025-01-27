using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Resources;
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
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalization;
        #endregion

        #region Constructor
        public StudentQueryHandler(IStudentService studentService,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalization) : base(stringLocalization)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalization = stringLocalization;
        }
        #endregion

        #region Handle Methods
        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            // Mapping using AutoMapper
            var studentList = await _studentService.GetStudentsListAsync();
            var studentListMapper = _mapper.Map<List<GetStudentListResponse>>(studentList);

            // result ( response )
            var result = Success(studentListMapper);
            result.Meta = new
            {
                Count = studentListMapper.Count
            };
            return result;
        }

        public async Task<Response<GetSingleStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentService.GetStudentByIdWithIncludeAsync(request.Id);
            if (student == null)
                return NotFound<GetSingleStudentResponse>(_stringLocalization[SharedResourcesKeys.NotFound]);
            //return NotFound<GetSingleStudentResponse>($"There is no Id = {request.Id}");

            var result = _mapper.Map<GetSingleStudentResponse>(student);
            return Success(result);

        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            // Make Expression to get students and put it into response 
            // - convert from student to GetStudentPaginatedListResponse
            Expression<Func<Student, GetStudentPaginatedListResponse>> expression =
                student => new GetStudentPaginatedListResponse(
                    student.Id,
                    student.Localize(student.NameAr, student.NameEn),
                    student.Address,
                    student.Department.Localize(student.Department.NameAr, student.Department.NameEn)
                    );

            var FilterQuery = _studentService.FilterStudentPaginatedQuerable(request.OrderBy, request.Search);

            var paginatedList = await FilterQuery
            .Select(expression) // convert from student to GetStudentPaginatedListResponse
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            // result ( response )
            paginatedList.Meta = new
            {
                Count = paginatedList.Data.Count
            };
            return paginatedList;

        }
        #endregion
    }
}
