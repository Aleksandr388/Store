using Store.DataAcess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public Task<Author> GetByName(string name);
        public Task RemoveAsync(Author model);
        public bool GetAllCreatedAuthors(IEnumerable<Author> models);
    }
}
