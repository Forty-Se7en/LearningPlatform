using LearningPlatform.DataAccess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DataAccess.Postgres.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .HasOne(a => a.Course)
                .WithOne(c => c.Author)
                .HasForeignKey<AuthorEntity>(a => a.CourseId);
        }
    }
}
