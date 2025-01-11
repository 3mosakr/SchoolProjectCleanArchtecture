using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrastructure.Data.Config
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(s => s.Id);

            //builder.Property(s => s.NameAr)
            //    .HasColumnType("NVARCHAR")
            //    .HasMaxLength(50)
            //    .IsRequired();

            builder.Property(s => s.NameEn)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            // Relationships
            // Required One to Many with StudentSubjects [Dependent (StudentSubjects) - Principal (Subject)] [Cascade Delete]
            builder.HasMany(s => s.StudentSubjects)
                .WithOne(x => x.Subject)
                .HasForeignKey(x => x.SubjectId)
                .IsRequired();

            // Required One to Many with DepartmentSubjects [Dependent (DepartmentSubjects) - Principal (Subject)] [Cascade Delete]
            builder.HasMany(s => s.DepartmentSubjects)
                .WithOne(x => x.Subject)
                .HasForeignKey(x => x.SubjectId)
                .IsRequired();

            builder.ToTable("Subjects");

        }
    }
}
