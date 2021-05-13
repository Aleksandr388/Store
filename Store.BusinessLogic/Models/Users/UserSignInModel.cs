using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Models.Users
{
    public class UserSignInModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "You entered an incorrect email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You entered an incorrect password")]
        public string Password { get; set; }
        [Display(Name = "Remember?")]
        public bool RememberMe { get; set; }
    }
}
