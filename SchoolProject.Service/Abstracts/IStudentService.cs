using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Service.Abstracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsListAsync();
        public Task<Student> GetByIdAsync(int id);
        public Task<Student> GetStudentByIdWithIncludeAsync(int id);
        public Task<string> AddAsync(Student student);
        public Task<string> EditAsync(Student student);
        public Task<string> DeleteAsync(Student student);
        public Task<bool> IsNameEnExist(string name);
        public Task<bool> IsNameArExist(string name);
        public Task<bool> IsNameEnExistExcludeSelf(string name, int id);
        public Task<bool> IsNameArExistExcludeSelf(string name, int id);

        public IQueryable<Student> GetStudentsQueryable();
        public IQueryable<Student> GetStudentsByDepartmentIdQueryable(int departmentId);
        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum, string search);

    }
}
