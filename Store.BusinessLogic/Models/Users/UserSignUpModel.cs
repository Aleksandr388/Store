using System.ComponentModel.DataAnnotations;

namespace Store.BusinessLogic.Models.Users
{
    public class UserSignUpModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = Shared.Constants.ErrorMessages.RegistrationFailedNoEmailInModel)]
        public string Email { get; set; }
        [Required(ErrorMessage = Shared.Constants.ErrorMessages.RegistrationFailedNoFirstNameInModel)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = Shared.Constants.ErrorMessages.RegistrationFailedNoLastNameInModel)]
        public string LastName { get; set; }
        [Required(ErrorMessage = Shared.Constants.ErrorMessages.RegistrationFailedNoPasswordInModel)]
        public string Password { get; set; }
        public string IsBlocked { get; set; }
    }
}
