using CursoOnline.Dominio.Data.Context;
using CursoOnline.Dominio.Repositories;

namespace CursoOnline.Dominio.Data
{
    public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(CourseDbContext ctx) : base(ctx) { }
    }
}
