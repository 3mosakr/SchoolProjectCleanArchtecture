using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        #endregion

        #region Handler Methodes

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository
                .GetTableNoTracking()
                .Where(d => d.Id.Equals(id))
                .Include(d => d.DepartmentSubjects).ThenInclude(ds => ds.Subject)

                .Include(d => d.Instructors)
                .Include(d => d.Instructor)
                .FirstOrDefaultAsync();

            return department;
        }

        #endregion
    }
}
