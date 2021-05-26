using Microsoft.EntityFrameworkCore;
using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.Base;
using Store.DataAcess.Repositories.Interfaces;
using Store.DataAcess.StoreContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.EFRepositories
{
    public class AuthorRepository : BaseEFRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ShopDbContext context) : base(context)
        {
        }

        public bool GetAllCreatedAuthors(IEnumerable<Author> models)
        {
            var result = models.All(x => _dbSet.Select(y => y).Contains(x));

            return result;
        }

        public async Task<Author> GetByName(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task RemoveAsync(Author model)
        {
            var removeModel = await _dbSet.FindAsync(model);

            removeModel.IsRemoved = true;

            await SaveChagesAsync();
        }
    }
}
