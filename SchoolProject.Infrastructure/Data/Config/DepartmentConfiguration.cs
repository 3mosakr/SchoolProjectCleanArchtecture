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
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
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
            // Required One to Many with DepartmentSubjects [Dependent (DepartmentSubjects) - Principal (DepartmentId)] [Cascade Delete]
            builder.HasMany(s => s.DepartmentSubjects)
                .WithOne(x => x.Department)
                .HasForeignKey(x => x.DepartmentId)
                .IsRequired();


            builder.ToTable("Departments");
        }
    }
}
