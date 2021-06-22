using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Users
{
    public class UserSignInModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = ErrorMessages.InvalidLoginName)]
        public string Email { get; set; }
        [Required(ErrorMessage = ErrorMessages.InvalidLoginPassword)]
        public string Password { get; set; }
        [Display(Name = DefaultValues.RememberPassword)]
        public bool RememberMe { get; set; }
    }
}
