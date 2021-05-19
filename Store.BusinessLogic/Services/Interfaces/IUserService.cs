using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Store.BusinessLogic.Models.Users;

namespace Store.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        public Task AddUserToRole(UserModel userModel);
        public Task CreateUserAsync(UserModel model);
        public Task UpdateUserAsync(UserModel model);
        public Task DeleteUserAsync(UserModel model);
        public Task<UserModel> GetByIdUserAsync(long id);
        public Task<IEnumerable<UserModel>> GetAllUserAsync();
        public Task<IEnumerable<UserModel>> GetAllUserAsync(Expression<Func<UserModel, bool>> predicate);
        public Task SaveChagesAsync();
    }
}
