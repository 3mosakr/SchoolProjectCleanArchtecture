﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entities
{
    public class Student
    {

        public int Id { get; set; }
        //public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; } = null!;

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
    }
}