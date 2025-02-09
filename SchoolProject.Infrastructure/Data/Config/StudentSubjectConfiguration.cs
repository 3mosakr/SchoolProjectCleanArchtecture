﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data.Config
{
    public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
    {
        public void Configure(EntityTypeBuilder<StudentSubject> builder)
        {
            builder.HasKey(d => new { d.StudentId, d.SubjectId });

            builder.Property(s => s.grade)
                .HasColumnType("decimal(5, 2)");

            builder.ToTable("StudentSubjects");
        }
    }
}
