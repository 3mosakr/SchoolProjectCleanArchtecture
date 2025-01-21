using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data.Config
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {

            builder.HasKey(i => i.Id);

            builder.Property(i => i.NameAr)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(i => i.NameEn)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(i => i.Address)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(i => i.Position)
                .HasColumnType("VARCHAR")
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(i => i.Salary)
                .HasColumnType("decimal(10, 2)")
                .IsRequired(false);

            builder.Property(i => i.DepartmentId).IsRequired(false);
            builder.Property(i => i.SupervisorId).IsRequired(false);

            // RelationShips
            // Required One to Many with DepartmentSubjects [Dependent (InstructorSubjects) - Principal (Instructor)] [Cascade Delete]
            builder.HasMany(i => i.InstructorSubjects)
                .WithOne(IS => IS.Instructor)
                .HasForeignKey(IS => IS.InstructorId)
                .IsRequired();


            // (Mange relation) with Department
            // Required One to One with Department [Dependent (Department) - Principal (Instructor)] [Cascade Delete]
            builder.HasOne(i => i.DepartmentManager)
                .WithOne(d => d.Instructor)
                .HasForeignKey<Department>(d => d.ManagerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            // (Supervising Relation) with himself
            // Required One to Many with DepartmentSubjects [Dependent (Instructor) - Principal (Instructor)] [Cascade Delete]
            builder.HasMany(i => i.Instructors)
                .WithOne(s => s.Supervisor)
                .HasForeignKey(s => s.SupervisorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);


            builder.ToTable("Instructors");
        }
    }
}
