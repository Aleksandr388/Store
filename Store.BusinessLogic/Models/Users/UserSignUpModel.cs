using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Models.Users
{
    public class UserSignUpModel
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirsName { get; set; }
        public string LastName { get; set; }
        public string Pasword { get; set; }
    }
}
