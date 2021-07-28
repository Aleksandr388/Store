using System.Threading.Tasks;
using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public Task UpdateUserAsync(UpdateUserModel model);
        public Task<UserModel> GetByIdUserAsync(string token);
        public Task SaveChagesAsync();
    }
}
