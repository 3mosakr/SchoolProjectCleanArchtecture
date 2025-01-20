using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities
{
    public class Subject : GeneralLocalizableEntity
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public int Period { get; set; }

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();

        public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; } = new List<DepartmentSubject>();

        //[InverseProperty("Subject")]
        //public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }
    }
}
