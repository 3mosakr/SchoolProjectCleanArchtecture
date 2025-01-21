using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities;

namespace SchoolProject.Infrastructure.Data.Config
{
    public class InstructorSubjectConfiguration : IEntityTypeConfiguration<InstructorSubject>
    {
        public void Configure(EntityTypeBuilder<InstructorSubject> builder)
        {
            builder.HasKey(x => new { x.InstructorId, x.SubjectId });

            builder.ToTable("InstructorSubjects");
        }
    }
}
