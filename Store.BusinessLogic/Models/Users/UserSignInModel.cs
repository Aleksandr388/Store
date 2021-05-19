using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Users
{
    public class UserSignInModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = Shared.Constants.ErrorMessages.InvalidLoginName)]
        public string Email { get; set; }
        [Required(ErrorMessage = Shared.Constants.ErrorMessages.InvalidLoginPassword)]
        public string Password { get; set; }
        [Display(Name = "Remember?")]
        public bool RememberMe { get; set; }
    }
}
