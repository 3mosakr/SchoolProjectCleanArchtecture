using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities
{
    public class Instructor : GeneralLocalizableEntity
    {

        public int Id { get; set; }
        public string NameEn { get; set; }
        public string? NameAr { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }


        // (Teach relation) with Department
        public int? DepartmentId { get; set; }
        public Department? Department { get; set; }

        // (Mange relation) with Department
        public Department? DepartmentManager { get; set; }

        public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; } = new List<InstructorSubject>();

        // (Supervising Relation) with himself
        public int? SupervisorId { get; set; }
        public Instructor? Supervisor { get; set; }

        public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    }
}
