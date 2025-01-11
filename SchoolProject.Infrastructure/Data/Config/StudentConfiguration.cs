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
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
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

            builder.Property(s => s.Address)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(s => s.Phone)
                .HasColumnType("VARCHAR")
                .HasMaxLength(15)
                .IsRequired();

            // Relationships
            // Required One to Many with Department [Dependent (Student) - Principal (Department)] [Cascade Delete]
            builder.HasOne(s => s.Department)
                .WithMany(x => x.Students)
                .HasForeignKey(s => s.DepartmentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            // Required One to Many with StudentSubjects [Dependent (StudentSubjects) - Principal (Student)] [Cascade Delete]
            builder.HasMany(s => s.StudentSubjects)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId)
                .IsRequired();


            builder.ToTable("Students");
        }
    }
}
