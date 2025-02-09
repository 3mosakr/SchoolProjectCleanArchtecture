﻿using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIdResponse>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)))
                .ForMember(dest => dest.MangerName,
                    opt => opt.MapFrom(src => src.Instructor.Localize(src.Instructor.NameAr, src.Instructor.NameEn)))
                .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(src => src.DepartmentSubjects))
                //.ForMember(dest => dest.StudentList, opt => opt.MapFrom(src => src.Students))
                .ForMember(dest => dest.InstructorList, opt => opt.MapFrom(src => src.Instructors))
                ;

            CreateMap<DepartmentSubject, SubjectResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubjectId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Subject.Localize(src.Subject.NameAr, src.Subject.NameEn)));

            //CreateMap<Student, StudentResponse>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));

            CreateMap<Instructor, InstructorResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Localize(src.NameAr, src.NameEn)));
        }
    }
}