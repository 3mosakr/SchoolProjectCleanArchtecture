using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Data.Config.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Address)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(u => u.Country)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(250)
                .IsRequired(false);
        }
    }
}
