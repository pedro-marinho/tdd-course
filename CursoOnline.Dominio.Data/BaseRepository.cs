using CursoOnline.Dominio.Data.Context;
using CursoOnline.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoOnline.Dominio.Data
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Entity
    {
        protected readonly CourseDbContext _ctx;

        public BaseRepository(CourseDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<T> GetById(int id)
        {
            return await _ctx.Set<T>().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<T>> Get()
        {
            return await _ctx.Set<T>().ToListAsync();
        }

        public async Task Add(T entity)
        {
            await _ctx.AddAsync(entity);
        }
    }
}
