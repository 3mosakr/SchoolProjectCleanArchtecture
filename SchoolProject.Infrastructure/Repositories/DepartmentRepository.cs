using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {

        #region Fields
        private readonly DbSet<Department> _department;
        #endregion

        #region Constructor
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _department = dbContext.Set<Department>();
        }
        #endregion

        #region Handle Methods
        //public async Task<List<Department>> GetAll()
        //{
        //}
        #endregion
    }

}
