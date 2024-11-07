using LearningPlatform.DataAccess.Postgres.Configurations;
using LearningPlatform.DataAccess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DataAccess.Postgres
{
    public class LearningDbContext (DbContextOptions <LearningDbContext> options) : DbContext(options)
    {       
        public DbSet<CourseEntity> Courses { get; set; }

        public DbSet<LessonEntity> Lessons { get; set; }

        public DbSet<AuthorEntity> Authors { get; set; }

        public DbSet<StudentEntity> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new LessonConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
