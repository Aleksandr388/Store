﻿using Store.BusinessLogic.Models.Users;
using Store.BusinessLogic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        public Task AddUserToRole(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public Task CreateUserAsync(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserModel>> GetAllUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserModel>> GetAllUserAsync(Expression<Func<UserModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetByIdUserAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChagesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
