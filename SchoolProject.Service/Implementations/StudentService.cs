using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    public class StudentService : IStudentService
    {
        #region Fields
        private readonly IStudentRepository _studentRepository;
        #endregion

        #region Constructor
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        
        #endregion

        #region Handler Methodes
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentRepository.GetStudentsListAsync();
        }
        
        public async Task<Student> GetStudentByIdAsync(int id)
        {
            //var student = await _studentRepository.GetByIdAsync(id);
            var student = _studentRepository.GetTableNoTracking()
                .Include(x=>x.Department)
                .Where(x => x.Id == id)
                . FirstOrDefault();

            return student;
        }

        public async Task<string> AddAsync(Student student)
        {
            // check if the name is Existing or not
            var StudentExist = _studentRepository
                .GetTableNoTracking()
                .Where(x => x.NameEn.Equals(student.NameEn))
                .FirstOrDefault();

            if (StudentExist != null)
                return "Exist";
            // validate is department is exist
            var isDepartmentExist = _studentRepository
                .GetTableNoTracking()
                .Include(x=>x.Department).Any(x => x.Department.Id == student.Id);

            if (!isDepartmentExist)
                return "Fail";

            // Add Student
            await _studentRepository.AddAsync(student);
            return "Success";
        }
        #endregion
    }
}
