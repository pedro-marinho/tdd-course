using CursoOnline.Dominio.Data.Context;
using CursoOnline.Dominio.Repositories;
using CursoOnline.Dominio.UoW;
using System.Threading.Tasks;

namespace CursoOnline.Dominio.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CourseDbContext _ctx;

        public UnitOfWork(CourseDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Commit()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
