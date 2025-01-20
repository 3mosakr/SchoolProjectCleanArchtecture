﻿using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities
{
    public class Student : LocalizableEntity
    {
        public int Id { get; set; }
        //public string NameEn { get; set; }
        //public string NameAr { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; } = null!;

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    }
}
