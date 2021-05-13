using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Models.Users
{
    public class UserSignUpModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "You entered an incorrect email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You entered an incorrect FirstName")]
        public string FirsName { get; set; }
        [Required(ErrorMessage = "You entered an incorrect LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "You entered an incorrect password")]
        public string Password { get; set; }
        public string IsBlocked { get; set; }
    }
}
