using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Store.DataAcess.Entities.Base;

namespace Store.DataAcess.Entities
{
    public class StoreUser : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsBlocked { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreationDate { get; set; }

        public StoreUser()
        {
            CreationDate = DateTime.Now;
        }
    }
}
