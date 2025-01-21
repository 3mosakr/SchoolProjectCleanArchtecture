using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities
{
    public class Department : GeneralLocalizableEntity
    {

        public int Id { get; set; }
        public string NameEn { get; set; }
        public string? NameAr { get; set; }

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();

        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; } = new List<DepartmentSubject>();


        // (Teach relation) with Instructor
        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();


        // (Mange relation) with Instructor
        public int? ManagerId { get; set; }
        public virtual Instructor? Instructor { get; set; }
    }
}
