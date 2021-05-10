using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.Base;
using Store.DataAcess.StoreContext;

namespace Store.DataAcess.Repositories.EFRepositories
{
    public class UserRepository : BaseEFRepository<StoreUser>
    {
        private readonly ShopDbContext _ctx;

        public UserRepository(ShopDbContext context) : base(context) 
        {
            _ctx = context;
        }
    }
}
