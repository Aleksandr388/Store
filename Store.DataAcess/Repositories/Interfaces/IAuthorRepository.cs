using Store.DataAcess.Entities;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public Task<Author> GetByName(string name);
    }
}
