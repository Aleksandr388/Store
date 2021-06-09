using Store.DataAcess.Entities;
using Store.DataAcess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.Interfaces
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        public Task<Author> GetByName(string name);
        public Task RemoveAsync(long model);
        public bool GetAllCreatedAuthors(IEnumerable<Author> models);
        public Task<IEnumerable<Author>> GetAllAuthorsAsync(AuthorFiltration author);
    }
}
