using Microsoft.EntityFrameworkCore;
using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.Base;
using Store.DataAcess.Repositories.Interfaces;
using Store.DataAcess.StoreContext;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAcess.Repositories.EFRepositories
{
    public class AuthorRepository : BaseEFRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ShopDbContext context) : base(context)
        {
        }

        public async Task<Author> GetByName(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
        } 
    }
}
