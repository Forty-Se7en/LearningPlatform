using LearningPlatform.DataAccess.Postgres.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DataAccess.Postgres.Repositories
{
    public class CoursesRepository
    {
        private readonly LearningDbContext _dbContext;

        public CoursesRepository(LearningDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<List<CourseEntity>> Get()
        {
            return await _dbContext.Courses
                .AsNoTracking()
                .OrderBy(c => c.Title)
                .ToListAsync();
        }


        public async Task<List<CourseEntity>> GetWithLessons()
        {
            return await _dbContext.Courses
                .AsNoTracking()
                .Include(c => c.Lessons)
                .OrderBy(c => c.Title)
                .ToListAsync();
        }

        public async Task<CourseEntity?> GetById(Guid id)
        {
            return await _dbContext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<CourseEntity>> GetByFilter(String title, decimal price)
        {
            var query = _dbContext.Courses.AsNoTracking();

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(c => c.Title.Contains(title));
            }

            if (price > 0)
            {
                query = query.Where(c => c.Price > price);
            }

            return await query.ToListAsync();
        }

        public async Task<List<CourseEntity>> GetByPage(int page, int pageSize)
        {
            return await _dbContext.Courses
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task Add(Guid id, Guid authorId, string title, string description, decimal price)
        {
            var courseEntity = new CourseEntity()
            { 
                Id = id,
                AuthorId = authorId,
                Title = title,
                Description = description,
                Price = price
            };

            await _dbContext.AddAsync(courseEntity);
            await _dbContext.SaveChangesAsync();
        }

        [Obsolete]        
        public async Task Update(Guid id, Guid authorId, string title, string description, decimal price)
        {
            var courseEntity = await _dbContext.Courses
                .FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new Exception();

            courseEntity.Title = title;
            courseEntity.Description = description;
            courseEntity.Price = price;

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateNew(Guid id, Guid authorId, string title, string description, decimal price)
        {
            await _dbContext.Courses
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.Title, title)
                    .SetProperty(c => c.Description, description)
                    .SetProperty(c => c.Id, id));
        }

        public async Task Delete(Guid id, Guid authorId, string title, string description, decimal price)
        {
            await _dbContext.Courses
                .Where(c => c.Id == id)
                .ExecuteDeleteAsync();
        }

    }
}
