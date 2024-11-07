using LearningPlatform.DataAccess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DataAccess.Postgres.Repositories
{
    public class LessonsRepository
    {
        private readonly LearningDbContext _dbContext;

        public LessonsRepository(LearningDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<LessonEntity?> GetById(Guid id)
        {
            return await _dbContext.Lessons
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<LessonEntity>> GetByFilter(String title)
        {
            var query = _dbContext.Lessons.AsNoTracking();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(c => c.Title.Contains(title));
            }
            return await query.ToListAsync();
        }

        public async Task Add(Guid courseId, LessonEntity lesson)
        {
            var courseEntity = await _dbContext.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId) ?? throw new Exception();
            courseEntity.Lessons.Add(lesson);
                        
            await _dbContext.SaveChangesAsync();
        }
        public async Task Add2(Guid courseId, string title)
        {
            var lessonEntity = new LessonEntity()
            {
                CourseId = courseId,
                Title = title
            };

            await _dbContext.AddAsync(lessonEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateNew(Guid id, Guid authorId, string title, string description, decimal price)
        {
            await _dbContext.Lessons
                .Where(l => l.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(l => l.Title, title)
                    .SetProperty(l => l.Description, description)
                    .SetProperty(c => c.Id, id));
        }

        public async Task Delete(Guid id, Guid authorId, string title, string description, decimal price)
        {
            await _dbContext.Lessons
                .Where(l => l.Id == id)
                .ExecuteDeleteAsync();
        }

    }
}
