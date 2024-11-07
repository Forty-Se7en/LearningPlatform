using LearningPlatform.DataAccess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.DataAccess.Postgres.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<StudentEntity>
    {
        public void Configure(EntityTypeBuilder<StudentEntity> builder)
        {
            builder.HasKey(s => s.Id);

            builder
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students);
        }
    }
}
