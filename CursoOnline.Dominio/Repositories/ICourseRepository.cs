using System.Threading.Tasks;

namespace CursoOnline.Dominio.Repositories
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<Course> GetByName(string name);
    }
}
