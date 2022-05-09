using CursoOnline.Dominio.Data.Context;
using CursoOnline.Dominio.Repositories;

namespace CursoOnline.Dominio.Data
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(CourseDbContext ctx) : base(ctx) { }
    }
}
