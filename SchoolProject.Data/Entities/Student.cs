using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities
{
    public class Student : GeneralLocalizableEntity
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string? NameAr { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public int? DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    }
}
