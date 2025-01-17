namespace SchoolProject.Core.Features.Students.Queries.Results
{
    public class GetStudentPaginatedListResponse
    {
        public int Id { get; set; }
        //public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Address { get; set; }

        public string DepartmentName { get; set; }

        public GetStudentPaginatedListResponse(int id, string nameEn, string address, string departmentName)
        {
            Id = id;
            NameEn = nameEn;
            Address = address;
            DepartmentName = departmentName;
        }
    }
}
