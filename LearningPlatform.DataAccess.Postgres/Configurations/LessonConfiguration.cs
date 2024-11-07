using LearningPlatform.DataAccess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LearningPlatform.DataAccess.Postgres.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<LessonEntity>
    {
        public void Configure(EntityTypeBuilder<LessonEntity> builder)
        {
            builder.HasKey(l => l.Id);

            builder
                .HasOne(l => l.Course)
                .WithMany(c => c.Lessons)
                .HasForeignKey(l => l.CourseId);
        }
    }
}
