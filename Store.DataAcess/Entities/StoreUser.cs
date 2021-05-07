using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Store.DataAcess.Entities.Base;

namespace Store.DataAcess.Entities
{
    public class StoreUser : IdentityUser<long>
    {
        public new long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string PasswordHash { get; set; }
        public override string Email { get; set; }

    }
}
