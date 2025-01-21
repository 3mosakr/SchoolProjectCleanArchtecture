using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data.Config
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
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

            builder.Property(s => s.Address)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(s => s.Phone)
                .HasColumnType("VARCHAR")
                .HasMaxLength(15)
                .IsRequired(false);

            builder.Property(s => s.DepartmentId)
                .IsRequired(false);

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
