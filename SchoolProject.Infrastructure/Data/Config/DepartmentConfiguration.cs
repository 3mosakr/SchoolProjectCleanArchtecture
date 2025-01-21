using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data.Config
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.NameAr)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(s => s.NameEn)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(d => d.ManagerId)
                .IsRequired(false);

            // Relationships
            // Required One to Many with DepartmentSubjects [Dependent (DepartmentSubjects) - Principal (DepartmentId)] [Cascade Delete]
            builder.HasMany(d => d.DepartmentSubjects)
                .WithOne(ds => ds.Department)
                .HasForeignKey(ds => ds.DepartmentId)
                .IsRequired();

            // (teach relation) with instructor
            // Required One to Many with DepartmentSubjects [Dependent (Instructors) - Principal (DepartmentId)] [Cascade Delete]
            builder.HasMany(d => d.Instructors)
                .WithOne(i => i.Department)
                .HasForeignKey(i => i.DepartmentId)
                .IsRequired(false);



            builder.ToTable("Departments");
        }
    }
}
