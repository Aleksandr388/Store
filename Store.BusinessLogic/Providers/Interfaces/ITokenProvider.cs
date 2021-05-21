using Store.BusinessLogic.Models.Users;
using Store.DataAcess.Entities;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Providers.Interfaces
{
    public interface ITokenProvider
    {
        public string CreateToken(StoreUser name, string role);
        public string CreateRefreshToken(int length);
        public Task<TokenModel> CreateNewTokensAync(TokenModel tokenModel);
    }
}
