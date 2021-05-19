using Store.DataAcess.Entities;

namespace Store.BusinessLogic.Providers.Interfaces
{
    public interface IJwtProvider
    {
        string CreateToken(StoreUser name, string role);
    }
}
