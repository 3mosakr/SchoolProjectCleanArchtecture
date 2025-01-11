using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Queries.Results
{
    // Dto
    public class GetStudentListResponse
    {
        public int Id { get; set; }
        //public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Address { get; set; }

        public string DepartmentName { get; set; }

    }
}
