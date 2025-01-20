using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities
{
    public class Department : GeneralLocalizableEntity
    {

        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();

        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; } = new List<DepartmentSubject>();

        //[InverseProperty("department")]
        //public virtual ICollection<Instructor> Instructors { get; set; }

        //public int InsManager { get; set; }
        //[ForeignKey("InsManager")]
        //[InverseProperty("departmentManager")]
        //public virtual Instructor? Instructor { get; set; }
    }
}
