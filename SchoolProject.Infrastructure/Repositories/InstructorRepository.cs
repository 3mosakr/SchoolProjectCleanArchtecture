using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Abstracts;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Repositories
{
    public class InstructorRepository : GenericRepositoryAsync<Instructor>, IInstructorRepository
    {
        #region Fields
        private readonly DbSet<Instructor> _instructor;
        #endregion

        #region Constructor
        public InstructorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _instructor = dbContext.Set<Instructor>();
        }
        #endregion

        #region Handle Methods
        //public async Task<List<Instructor>> GetAll()
        //{
        //}
        #endregion
    }
}
