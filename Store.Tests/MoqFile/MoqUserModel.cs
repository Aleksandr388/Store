using Store.BusinessLogic.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests.MoqFile
{
    public static class MoqUserModel
    {
        public static UserSignInModel GetUserSignIn()
        {
            UserSignInModel user = new UserSignInModel()
            {
                Email = "",
                Id = 1,
                Password = "",
                RememberMe = true
            };
            return user;
        }
    }
}
