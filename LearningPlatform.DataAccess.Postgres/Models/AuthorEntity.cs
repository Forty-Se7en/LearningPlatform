using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningPlatform.DataAccess.Postgres.Models
{
    public class AuthorEntity
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public CourseEntity Course { get; set; }

        public Guid CourseId { get; set; }
    }

}
