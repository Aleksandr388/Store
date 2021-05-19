using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Users
{
    public class ForgotPasswordUser
    {
        [Required(ErrorMessage = Shared.Constants.ErrorMessages.InvalidLoginName)]
        public string Email { get; set; }
    }
}
