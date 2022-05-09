using CursoOnline.Dominio.Repositories;
using System.Threading.Tasks;

namespace CursoOnline.Dominio.UoW
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
