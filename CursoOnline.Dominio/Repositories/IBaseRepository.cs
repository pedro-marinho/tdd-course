using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoOnline.Dominio.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task<List<T>> Get();
        Task Add(T entity);
    }
}
