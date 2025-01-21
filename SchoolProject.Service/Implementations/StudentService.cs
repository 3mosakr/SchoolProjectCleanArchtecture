using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Service.Abstracts;

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

        public async Task<Student> GetStudentByIdWithIncludeAsync(int id)
        {
            var student = _studentRepository.GetTableNoTracking()
                .Include(x => x.Department)
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return student;
        }

        public async Task<string> AddAsync(Student student)
        {

            //// validate is department is exist 

            // Add Student
            await _studentRepository.AddAsync(student);
            return "Success";
        }

        public async Task<bool> IsNameEnExist(string name)
        {
            // check if the name is Existing or not
            var StudentExist = _studentRepository
                .GetTableNoTracking()
                .Where(x => x.NameEn.Equals(name))
                .FirstOrDefault();

            if (StudentExist == null)
                return false;
            return true;
        }

        public async Task<bool> IsNameArExist(string name)
        {
            // check if the name is Existing or not
            var StudentExist = _studentRepository
                .GetTableNoTracking()
                .Where(x => x.NameAr.Equals(name))
                .FirstOrDefault();

            if (StudentExist == null)
                return false;
            return true;
        }

        public async Task<bool> IsNameEnExistExcludeSelf(string name, int id)
        {
            // check if the name is Existing or not
            var StudentExist = await _studentRepository
                .GetTableNoTracking()
                .Where(x => x.NameEn.Equals(name) & !x.Id.Equals(id))
                .FirstOrDefaultAsync();

            if (StudentExist == null)
                return false;
            return true;
        }

        public async Task<bool> IsNameArExistExcludeSelf(string name, int id)
        {
            // check if the name is Existing or not
            var StudentExist = await _studentRepository
                .GetTableNoTracking()
                .Where(x => x.NameAr.Equals(name) & !x.Id.Equals(id))
                .FirstOrDefaultAsync();

            if (StudentExist == null)
                return false;
            return true;
        }

        public async Task<string> EditAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "Success";
        }

        public async Task<string> DeleteAsync(Student student)
        {
            var transaction = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                await transaction.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return "Failed";
            }

        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            return student;
        }

        public IQueryable<Student> GetStudentsQueryable()
        {
            return _studentRepository
                .GetTableNoTracking()
                .Include(x => x.Department)
                .AsQueryable();
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(StudentOrderingEnum orderingEnum, string search)
        {
            var querable = _studentRepository
                .GetTableNoTracking()
                .Include(x => x.Department)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                querable = querable.Where(x => x.NameEn.Contains(search) || x.Address.Contains(search));
            }

            switch (orderingEnum)
            {
                case StudentOrderingEnum.Id:
                    querable = querable.OrderBy(x => x.Id);
                    break;
                case StudentOrderingEnum.NameEn:
                    querable = querable.OrderBy(x => x.NameEn);
                    break;
                case StudentOrderingEnum.Address:
                    querable = querable.OrderBy(x => x.Address);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    querable = querable.OrderBy(x => x.Department.NameEn);
                    break;
                default:
                    querable = querable.OrderBy(x => x.Id);
                    break;
            }

            return querable;
        }
        #endregion
    }
}
