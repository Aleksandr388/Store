using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Store.DataAcess.Entities
{
    public class StoreUser : IdentityUser<long>
    {
        [NotNull]
        public string FullName { get; set; }
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
