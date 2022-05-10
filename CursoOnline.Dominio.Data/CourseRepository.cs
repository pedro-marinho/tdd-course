using CursoOnline.Dominio.Data.Context;
using CursoOnline.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Dominio.Data
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(CourseDbContext ctx) : base(ctx) { }

        public async Task<Course> GetByName(string name)
        {
            return await _ctx.Courses.Where(c => c.Name == name).FirstOrDefaultAsync();
        }

        public async Task<Course> GetByIdWithEnrollments(int id)
        {
            return await _ctx.Courses.Where(c => c.Id == id)
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync();
        }
    }
}
